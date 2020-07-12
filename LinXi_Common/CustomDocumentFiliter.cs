using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinXi_Common
{
    public class CustomDocumentFiliter /*: IDocumentFilter*/
    {
        //public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        //{
        //    SetContorllerDescription(swaggerDoc.Extensions);
        //}

        //private void SetContorllerDescription(Dictionary<string, object> extensionsDict)
        //{
        //    string _xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "HKERP.Application.xml");
        //    ConcurrentDictionary<string, string> _controllerDescDict = new ConcurrentDictionary<string, string>();

        //    if (File.Exists(_xmlPath))
        //    {
        //        XmlDocument _xmlDoc = new XmlDocument();
        //        _xmlDoc.Load(_xmlPath);

        //        string _type = string.Empty, _path = string.Empty, _controllerName = string.Empty;
        //        XmlNode _summaryNode = null;

        //        foreach (XmlNode _node in _xmlDoc.SelectNodes("//member"))
        //        {
        //            _type = _node.Attributes["name"].Value;

        //            if (_type.StartsWith("T:") && !_type.Contains("T:HKERP.HKERPAppServiceBase") && !_type.Contains("T:HKERP.Net.MimeTypes.MimeTypeNames"))
        //            {
        //                _summaryNode = _node.SelectSingleNode("summary");
        //                string[] _names = _type.Split('.');
        //                string _key = _names[_names.Length - 1];
        //                if (_key.IndexOf("AppService", _key.Length - "AppService".Length, StringComparison.Ordinal) > -1)
        //                {
        //                    _key = _key.Substring(0, _key.Length - "AppService".Length);
        //                }

        //                if (_summaryNode != null && !string.IsNullOrEmpty(_summaryNode.InnerText) && !_controllerDescDict.ContainsKey(_key))
        //                {
        //                    _controllerDescDict.TryAdd(_key, _summaryNode.InnerText.Trim());
        //                }
        //            }
        //        }

        //        extensionsDict.TryAdd("ControllerDescription", _controllerDescDict);
        //    }
        //}
    }
}