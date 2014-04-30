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
      buffer.AppendLine("<?xml version=\"1.0\"?>");
      buffer.AppendLine("<!--DOCTYPE coverage SYSTEM \"http://cobertura.sourceforge.net/xml/coverage-03.dtd\"-->");
      buffer.AppendLine("<coverage>");
      buffer.AppendLine("\t<packages>");
      
      foreach (PackageElement pe in project.GetPackages())
      {
        buffer.AppendFormat("\t\t<package name=\"{0}\">\r\n", pe.Name);
        buffer.AppendLine("\t\t\t<classes>");

        foreach (FileElement fe in pe.GetFiles())
        {
          foreach (ClassElement ce in fe.GetClasses())
          {
            float classLineRate = ce.Metrics.CoveredElements / (float)ce.Metrics.Elements;
 
            buffer.AppendFormat("\t\t\t\t<class name=\"{0}\" filename=\"{1}\" line-rate=\"{2}\">\r\n", ce.Name, fe.Name, classLineRate);

            buffer.AppendLine("\t\t\t\t\t<methods>");
            var methodLines = fe.GetLines().GroupBy(l => l.Signature);
            foreach(var method in methodLines)
            {
              float methodLineRate = method.Count(l => l.Coverage < 2) / (float)method.Count();
              buffer.AppendLine(string.Format("\t\t\t\t\t\t<method name=\"{0}\" signature=\"{0}\" line-rate=\"{1}\">", method.Key, methodLineRate));
              buffer.AppendLine("\t\t\t\t\t\t\t<lines>");
              foreach(LineElement le in method)
              {
                int visits = le.Coverage < 2 ? 1 : 0;
                buffer.AppendLine(string.Format("\t\t\t\t\t\t\t\t<line number=\"{0}\" hits=\"{1}\" branch=\"false\" />", le.Number, visits));
              }
              buffer.AppendLine("\t\t\t\t\t\t\t</lines>");
              buffer.AppendLine("\t\t\t\t\t\t</method>");
            }
            buffer.AppendLine("\t\t\t\t\t</methods>");
            

            buffer.AppendLine("\t\t\t\t\t<lines>");
            foreach (LineElement le in fe.GetLines())
            {
              int visits = le.Coverage < 2 ? 1 : 0;
              buffer.AppendFormat("\t\t\t\t\t\t<line number=\"{0}\" hits=\"{1}\" branch=\"{2}\"/>\r\n", le.Number, visits, false);
            }
            buffer.AppendLine("\t\t\t\t\t</lines>");


            buffer.AppendLine("\t\t\t\t</class>");
          }
        }
        buffer.AppendLine("\t\t\t</classes>");
        buffer.AppendLine("\t\t</package>");
      }

      buffer.AppendLine("\t</packages>");
      buffer.AppendLine("</coverage>");
      return buffer.ToString();
    }
  }
}
