using SuperService_BackEnd;
using SuperService_BackEnd.Models;
using SuperService_BackEnd.ServiceUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperService_BusinessLayer
{
    public class ItemHelper
    {
        ItemService _serv = new ItemService();
        public IEnumerable<Item> GetAllItemsOrderedByAvailability() => _serv.GetAllItems().OrderBy(x => x.CanOrder).ThenBy(x => x.Name);
        public Item GetItemByID(int id) => _serv.GetItemByID(id);
    }
}
