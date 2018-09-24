using ConsignmentShopClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_1___Consignment_Shop
{
    public partial class ConsignmentShop : Form
    {
        private Store store = new Store();
        private List<Item> shoppingCartItems = new List<Item>();
        private decimal storeProfit = 0;
        BindingSource itemsBinding = new BindingSource();
        BindingSource cartItemsBinding = new BindingSource();
        BindingSource vendorBinding = new BindingSource();


        public ConsignmentShop()
        {
            InitializeComponent();
            SetUpData();

            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();
            itemsListBox.DataSource = itemsBinding;
            itemsListBox.DisplayMember = "Display";
            itemsListBox.ValueMember = "Display";

            cartItemsBinding.DataSource = shoppingCartItems;
            shoppingCartListBox.DataSource = cartItemsBinding;
            shoppingCartListBox.DisplayMember = "Display";
            shoppingCartListBox.ValueMember = "Display";

            vendorBinding.DataSource = store.Vendors;
            vendorListBox.DataSource = vendorBinding;
            vendorListBox.DisplayMember = "Display";
            vendorListBox.ValueMember = "Display";
            

        }
        private void SetUpData()
        {
            store.Name = "Shop1";

            store.Vendors.Add(new Vendor { FirstName = "Jill", LastName = "Smith"});
            store.Vendors.Add(new Vendor { FirstName = "Bill", LastName = "Brown"});

            store.Items.Add(new Item
            {
                Title = "Book1",
                Description = "Book1 decsription",
                Price = 4.50M,
                Owner = store.Vendors[0]
            });
            store.Items.Add(new Item
            {
                Title = "Book2",
                Description = "Book2 decsription",
                Price = 3.80M,
                Owner = store.Vendors[0]
            });
            store.Items.Add(new Item
            {
                Title = "Book3",
                Description = "Book3 decsription",
                Price = 2.50M,
                Owner = store.Vendors[1]
            });
            store.Items.Add(new Item
            {
                Title = "Book4",
                Description = "Book4 decsription",
                Price = 5.50M,
                Owner = store.Vendors[1]
            });
        }

        private void addToCartButton_Click(object sender, EventArgs e)
        {
            if (store.Items.Count != 0)
            {
                Item selectedItem = (Item)itemsListBox.SelectedItem;
                shoppingCartItems.Add(selectedItem);
                selectedItem.Sold = true;
                store.Items.Remove(selectedItem);
                cartItemsBinding.ResetBindings(false);
                itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();
                itemsBinding.ResetBindings(false);
            }
        }

        private void purchaseButton_Click(object sender, EventArgs e)
        {
            foreach(Item item in shoppingCartItems)
            {
                //item.Sold = true;
                item.Owner.PaymentDue += (decimal)item.Owner.Commission * item.Price;
                storeProfit += (1-(decimal)item.Owner.Commission) * item.Price;
            }
            totalStoreMoney.Text = string.Format("${0}",storeProfit);
            shoppingCartItems.Clear();
            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();
            cartItemsBinding.ResetBindings(false);
            itemsBinding.ResetBindings(false);
            vendorBinding.ResetBindings(false);
        }


        private void removeButton_Click(object sender, EventArgs e)
        {
            if (shoppingCartItems.Count != 0)
            {
                Item removeItem = (Item)shoppingCartListBox.SelectedItem;
                shoppingCartItems.Remove(removeItem);
                removeItem.Sold = false;
                store.Items.Add(removeItem);
                itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();
                itemsBinding.ResetBindings(false);
                cartItemsBinding.ResetBindings(false);
            }
        }
    }
}
