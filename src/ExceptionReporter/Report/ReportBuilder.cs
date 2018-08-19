using ExceptionReporting.SystemInfo;
using ExceptionReporting.Templates;

namespace ExceptionReporting.Report
{
	internal class ReportBuilder
	{
		private readonly ExceptionReportInfo _info;
		private readonly IAssemblyDigger _assemblyDigger;
		private readonly IStackTraceMaker _stackTraceMaker;
		private readonly ISysInfoResultMapper _sysInfoMapper;

		public ReportBuilder(ExceptionReportInfo info,
			IAssemblyDigger assemblyDigger, IStackTraceMaker stackTraceMaker, ISysInfoResultMapper sysInfoMapper)
		{
			_info = info;
			_assemblyDigger = assemblyDigger;
			_stackTraceMaker = stackTraceMaker;
			_sysInfoMapper = sysInfoMapper;
		}

		public string Report(TemplateFormat format = TemplateFormat.Text)
		{
			var renderer = new TemplateRenderer(this.Model());
			return renderer.Render(format);
		}
		
		public ReportModel Model()
		{
			return new ReportModel
			{
				App = new App
				{
					Name = _info.AppName,
					Version= _info.AppVersion,
					Region = _info.RegionInfo,
					User = _info.UserName,
					AssemblyRefs = _assemblyDigger.GetAssemblyRefs()
				},
				SystemInfo = _sysInfoMapper.CreateTreeString(),
				Error = new Error
				{
					Exception = _info.MainException,
					Explanation = _info.UserExplanation,
					When = _info.ExceptionDate,
					FullStackTrace = _stackTraceMaker.FullStackTrace()
				}
			};
		}
	}
}
