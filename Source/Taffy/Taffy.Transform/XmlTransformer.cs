using System.Collections.Generic;
using System.Xml;

namespace Taffy.Transform {
    public class XmlTransformer : IXmlTransformer {
        private const string RssUrlAttributeName = "url";
        private const string RssLengthAttributeName = "length";
        private readonly IUrlTransformer _urlTransformer;

        public XmlTransformer(IUrlTransformer urlTransformer) {
            _urlTransformer = urlTransformer;
        }

        public XmlDocument GetTransformedXmlDocument(XmlDocument xmlDocument, Dictionary<string, string> xmlNamespacesToUseWhenSelectingNodes, List<string> xPathsOfNodesToRedirect, string destinationRelativeUrlBase) {
            var xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
            foreach (var xmlNamespace in xmlNamespacesToUseWhenSelectingNodes) {
                xmlNamespaceManager.AddNamespace(xmlNamespace.Key, xmlNamespace.Value);
            }
            foreach (var xPathOfNodesToRedirect in xPathsOfNodesToRedirect) {
                var nodesToRedirect = xmlDocument.SelectNodes(xPathOfNodesToRedirect, xmlNamespaceManager);
                RedirectUrlNodes(nodesToRedirect);
            }
            return xmlDocument;
        }

        private void RedirectUrlNodes(XmlNodeList enclosureNodes) {
            if (enclosureNodes != null) {
                foreach (XmlNode enclosureNode in enclosureNodes) {
                    var urlAttribute = enclosureNode.Attributes[RssUrlAttributeName];
                    RedirectUrlValue(urlAttribute);
                    var lengthAttribute = enclosureNode.Attributes[RssLengthAttributeName];
                    if (lengthAttribute != null) {
                        lengthAttribute.Value = "0";
                    }
                }
            }
        }

        private void RedirectUrlValue(XmlNode xmlAttribute) {
            if (xmlAttribute != null) {
                var originalUrl = xmlAttribute.Value;
                var newAttributeValue = _urlTransformer.GetFileUrl(originalUrl);
                xmlAttribute.Value = newAttributeValue;
            }
        }
    }

    public interface IXmlTransformer {
        XmlDocument GetTransformedXmlDocument(XmlDocument xmlDocument, Dictionary<string, string> xmlNamespacesToUseWhenSelectingNodes, List<string> xPathsOfNodesToRedirect, string destinationRelativeUrlBase);
    }
}