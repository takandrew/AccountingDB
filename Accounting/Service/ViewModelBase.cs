using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Service
{
    public abstract class ViewModelBase
    {
        private InsertUpdateWindow _insertUpdateWindow = null;

        protected void ShowInsertUpdateWindow(ViewModelBase viewModel)
        {
            viewModel._insertUpdateWindow = new InsertUpdateWindow()
            {
                DataContext = viewModel
            };
            viewModel._insertUpdateWindow.Closed += (sender, e) => Closed();
            viewModel._insertUpdateWindow.Show();
        }

        protected virtual void Closed()
        {

        }

        public bool CloseInsertUpdateWindow()
        {
            var result = false;
            if (_insertUpdateWindow != null)
            {
                _insertUpdateWindow.Close();
                _insertUpdateWindow = null;
                result = true;
            }

            return result;
        }
    }
}
