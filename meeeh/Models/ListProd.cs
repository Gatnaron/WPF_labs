using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Data.SqlClient;
using System.Data;

using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.SqlServer;
using meeeh.Models;


namespace meeeh.Models
{
    public class ListProd : ObservableCollection<Product>
    {
        public ListProd()
        {
            var queryRole = from prod in Page1.DataEntitiesEmployee.Products select prod;
            foreach (Product prod in queryRole.ToList())
            {
                this.Add(prod);
            }
        }
    }
}
