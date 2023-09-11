using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.Base
{
    public class BaseModel : IBaseModel
    {
        public int Id { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime? UpdatedDate { get; set; }
        public virtual int? CreatedBy { get; set; }
        public virtual int? UpdatedBy { get; set; }
        [NotMapped]
        public virtual string CreatedByFullName { get; set; }
        [NotMapped]
        public virtual string UpdatedByFullName { get; set; }

    }

    public class BaseModel<TKey> : BaseModel, IBaseModel<TKey>
    {
        public virtual TKey Id { get; set; }
    }
}
