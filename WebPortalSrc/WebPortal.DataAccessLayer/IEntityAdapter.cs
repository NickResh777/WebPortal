using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer {
    public interface IEntityAdapter {
        /// <summary>
        /// Get the entity within adapter
        /// </summary>
        BaseEntity Entity { get; set; }
    }
}
