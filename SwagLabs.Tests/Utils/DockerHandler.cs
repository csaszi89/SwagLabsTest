using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using SwagLabs.Tests.Definitions;

namespace SwagLabs.Tests.Utils
{
    internal class DockerHandler
    {
        private readonly BrowserType _browserType;
        private DotNet.Testcontainers.Networks.INetwork? _gridNetwork;
        private IContainer? _hubContainer;
        private IContainer? _nodeContainer;
        private readonly string _networkName = "selenium-grid-network";
        private readonly string _hubName = "selenium-hub";

        public DockerHandler(BrowserType browserType)
        {
            _browserType = browserType;
        }

        public async Task InitializeNetwork()
        {
            _gridNetwork = new NetworkBuilder()
                .WithName(_networkName)
                .Build();
            await _gridNetwork.CreateAsync();
        }

        public async Task InitializeHub()
        {
            _hubContainer = new ContainerBuilder()
                .WithImage("selenium/hub:4.8.0-20230210")
                .WithName("selenium-hub")
                .WithPortBinding("4442", "4442")
                .WithPortBinding("4443", "4443")
                .WithPortBinding("4444", "4444")
                .WithNetwork(_gridNetwork)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(4444))
                .Build();
            await _hubContainer.StartAsync();
        }

        public async Task InitializeNode()
        {
            var environment = new Dictionary<string, string>
            {
                { "SE_EVENT_BUS_HOST", _hubName },
                { "SE_EVENT_BUS_PUBLISH_PORT", "4442" },
                { "SE_EVENT_BUS_SUBSCRIBE_PORT", "4443" }
            };

            _nodeContainer = new ContainerBuilder()
                .WithImage(GetImageName())
                .WithEnvironment(environment)
                .WithNetwork(_gridNetwork)
                .Build();

            await _nodeContainer.StartAsync();
        }

        private string GetImageName()
        {
            switch (_browserType)
            {
                case BrowserType.Chrome:
                    return "selenium/node-chrome:4.8.0-20230210";
                case BrowserType.Edge:
                    return "selenium/node-edge:4.8.0-20230210";
                default:
                    throw new NotSupportedException("Browser not supported");
            }
        }
    }
}
