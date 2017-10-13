using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using HandlebarsDotNet;
using Xunit;

namespace HandlebarsTests
{
	public class Tests
	{
		[Fact]
		public async Task Handlebars_Compile_Test()
		{
			var list = new List<Task>();
			for (var i = 0; i < 100; i++)
				list.Add( RunPrecompiled() );

			await Task.WhenAll( list );
		}

		async Task RunPrecompiled()
		{
			var html = await ReadFileAsync( "template.html" );
			var handleBars = Handlebars.Create();
			var template = handleBars.Compile( html );
			template( new { } );
		}

		private async Task<string> ReadFileAsync( string fileName )
		{
			using (var fileReader = new FileStream( fileName, FileMode.Open, FileAccess.Read, FileShare.Read ))
			using (var streamReader = new StreamReader( fileReader ))
			{
				var result = await streamReader.ReadToEndAsync();
				return result;
			}
		}
	}
}