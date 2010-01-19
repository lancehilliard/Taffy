using System.Collections.Generic;
using System.Xml;

namespace Taffy.Transform {
    public class Xml {
        public static XmlDocument GetTransformedXmlDocument(XmlDocument xmlDocument, Dictionary<string, string> xmlNamespacesToUseWhenSelectingNodes, List<string> xPathsOfNodesToRedirect, string destinationRelativeUrlBase) {
            var xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
            foreach (var xmlNamespace in xmlNamespacesToUseWhenSelectingNodes) {
                xmlNamespaceManager.AddNamespace(xmlNamespace.Key, xmlNamespace.Value);
            }
            foreach (var xPathOfNodesToRedirect in xPathsOfNodesToRedirect) {
                var nodesToRedirect = xmlDocument.SelectNodes(xPathOfNodesToRedirect, xmlNamespaceManager);
                RedirectUrlNodes(nodesToRedirect, destinationRelativeUrlBase);
            }
            return xmlDocument;
        }

        private static void RedirectUrlNodes(XmlNodeList enclosureNodes, string destinationRelativeUrlBase) {
            if (enclosureNodes != null) {
                foreach (XmlNode enclosureNode in enclosureNodes) {
                    var urlAttribute = enclosureNode.Attributes["url"];
                    RedirectUrlValue(urlAttribute, destinationRelativeUrlBase);
                }
            }
        }

        private static void RedirectUrlValue(XmlNode xmlAttribute, string destinationRelativeUrlBase) {
            if (xmlAttribute != null) {
                var originalUrl = xmlAttribute.Value;
                var newAttributeValue = Url.GetFileUrl(originalUrl); // GetFileUrlThatTakesAdvantageOfUrlRewriting(originalUrl);
                xmlAttribute.Value = newAttributeValue;
            }
        }
    }
}