using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleClickClose
{
    using System.ComponentModel.Composition;
    using Microsoft.VisualStudio.Text.Editor;
    using Microsoft.VisualStudio.Utilities;

    [Export(typeof(IMouseProcessorProvider))]
    [Name("MiddleClickClose")]
    [Order(Before = "UrlClickMouseProcessor")]
    [ContentType("text")]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    [TextViewRole(PredefinedTextViewRoles.Editable)]
    internal sealed class MiddleClickCloseFactory : IMouseProcessorProvider
    {
        internal const string ADORNER_LAYER_NAME = "MiddleClickClickLayer";

        [Export]
        [Name(ADORNER_LAYER_NAME)]
        [Order(Before = PredefinedAdornmentLayers.Selection)]
        internal AdornmentLayerDefinition viewLayerDefinition;

        public IMouseProcessor GetAssociatedProcessor(IWpfTextView textView)
        {
            return MiddleClickClose.Create(textView);
        }
    }
}
