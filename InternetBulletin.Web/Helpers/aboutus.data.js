import DotNetImg from "@assets/AboutUsImg/Dotnet-abs.png";
import SQLImg from "@assets/AboutUsImg/sqlserver-img.png";
import AzureImg from "@assets/AboutUsImg/azure-img.png";
import ReactImg from "@assets/AboutUsImg/react-img.png";
import FluentUIImg from "@assets/AboutUsImg/fluentui-img.png";

const AboutUsData = [
	{
		Heading: ".NET",
		Description:
			"NET is a free, cross-platform, open-source developer platform used for building various types of applications, including web apps, mobile apps, desktop apps, and more. It is maintained by Microsoft and the .NET Foundation on GitHub.",
		Image: DotNetImg,
		Link: "https://dotnet.microsoft.com/en-us/",
	},
	{
		Heading: "Microsoft SQL Server",
		Description: `
		Get the flexibility you need to use integrated solutions and apps with your data—in the cloud, on-premises, or at the edge.
		SQL Server 2022 is the most Azure-enabled release yet, with innovation across performance, security, and availability`,
		Image: SQLImg,
		Link: "https://www.microsoft.com/en-in/sql-server",
	},
	{
		Heading: "Azure PaaS",
		Description:
			"Microsoft Azure, or just Azure is the cloud computing platform developed by Microsoft. It has management, access and development of applications and services to individuals, companies, and governments through its global infrastructure",
		Image: AzureImg,
		Link: "https://azure.microsoft.com/",
	},
	{
		Heading: "React 19",
		Description:
			'React is a free and open-source front-end JavaScript library that aims to make building user interfaces based on components more "seamless". It is maintained by Meta and a community of individual developers and companies.',
		Image: ReactImg,
		Link: "https://react.dev",
	},
	{
		Heading: "Fluent UI",
		Description: `
		Fluent UI is Microsoft's design system and component library that provides a unified visual language and user experience across Microsoft products and services.`,
		Image: FluentUIImg,
		Link: "https://fluent2.microsoft.design/components/web/react/",
	},
];

export { AboutUsData };
