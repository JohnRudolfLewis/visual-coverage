using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualCoverage.Core.Elements;

namespace VisualCoverage.Core
{
  public class CoberturaReport
  {
    public CoberturaReport()
    {

    }

    public virtual String Execute(ProjectElement project)
    {
      StringBuilder buffer = new StringBuilder();
      buffer.Append("<?xml version=\"1.0\"?>");
      buffer.Append("<!--DOCTYPE coverage SYSTEM \"http://cobertura.sourceforge.net/xml/coverage-03.dtd\"-->");
      buffer.Append("<coverage>");

      buffer.Append("</coverage>");
      return buffer.ToString();
    }
  }
}
