using System;
using System.Collections.Generic;
using System.Xml;
using Taffy.Configuration;

namespace Taffy.Transform {
    public class XmlTransformer : IXmlTransformer {
        private const string RssUrlAttributeName = "url";
        private const string RssLengthAttributeName = "length";
        private readonly IUrlTransformer _urlTransformer;
        private const string XmlUrlAttributeName = "xmlUrl";

        public XmlTransformer(IUrlTransformer urlTransformer) {
            _urlTransformer = urlTransformer;
        }

        public XmlDocument GetTransformedFeedXmlDocument(XmlDocument xmlDocument, Dictionary<string, string> xmlNamespacesToUseWhenSelectingNodes, List<string> xPathsOfNodesToRedirect, string destinationRelativeUrlBase) {
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

        public XmlDocument GetTransformedOpmlXmlDocument(XmlDocument opmlXmlDocument, string taffyAddress) {
            if (opmlXmlDocument != null) {
                var outlineNodes = opmlXmlDocument.SelectNodes("//outline[@xmlUrl!='']");
                if (outlineNodes != null) {
                    foreach (XmlNode outlineNode in outlineNodes) {
                        var xmlUrlAttribute = outlineNode.Attributes[XmlUrlAttributeName];
                        if (xmlUrlAttribute != null) {
                            var taffyFeedXmlUrl = _urlTransformer.GetFeedUrl(xmlUrlAttribute.Value, taffyAddress);
                            xmlUrlAttribute.Value = taffyFeedXmlUrl;
                        }
                    }
                }
            }
            return opmlXmlDocument;
        }

        private void RedirectUrlNodes(XmlNodeList enclosureNodes) {
            if (enclosureNodes != null) {
                foreach (XmlNode enclosureNode in enclosureNodes) {
                    var urlAttribute = enclosureNode.Attributes[RssUrlAttributeName];
                    RedirectUrlValue(urlAttribute);
                    var lengthAttribute = enclosureNode.Attributes[RssLengthAttributeName];
                    if (lengthAttribute != null) {
                        lengthAttribute.Value = Constants.RssUnknownLength;
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
        XmlDocument GetTransformedFeedXmlDocument(XmlDocument xmlDocument, Dictionary<string, string> xmlNamespacesToUseWhenSelectingNodes, List<string> xPathsOfNodesToRedirect, string destinationRelativeUrlBase);
        XmlDocument GetTransformedOpmlXmlDocument(XmlDocument opmlXmlDocument, string taffyAddress);
    }
}