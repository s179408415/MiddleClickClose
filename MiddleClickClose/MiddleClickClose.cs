using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;

namespace MiddleClickClose
{
    internal sealed class MiddleClickClose : MouseProcessorBase
    {
        public static IMouseProcessor Create(IWpfTextView view)
        {
            return view.Properties.GetOrCreateSingletonProperty(() => new MiddleClickClose(view));
        }

        private MiddleClickClose(IWpfTextView view)
        {
            View = view;
            Layer = view.GetAdornmentLayer(MiddleClickCloseFactory.ADORNER_LAYER_NAME);
            View.Closed += OnClosed;
        }
        public IWpfTextView View { get; }

        public IAdornmentLayer Layer { get; }

        private void OnClosed(object sender, EventArgs e)
        {
            View.Closed -= OnClosed;
        }
        public override void PreprocessMouseDown(MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle)
            {
                ITextDocument textDocument;
                if (View.TextBuffer.Properties.TryGetProperty(typeof(ITextDocument), out textDocument))
                {

                    var serviceProvider = (IServiceProvider)ServiceProvider.GlobalProvider;
                    DTE dte = (DTE)serviceProvider.GetService(typeof(DTE));
                    
                    var documentWindow = dte.Windows.OfType<Window>()
                            .FirstOrDefault(window => window.Document != null && window.Document.FullName == textDocument.FilePath);
                    if (documentWindow != null)
                    {
                        documentWindow.Close();
                    }
                    else
                    {
                        SendKeys.Send("{ESC}");
                    }
                }
            }
        }
    }
}
