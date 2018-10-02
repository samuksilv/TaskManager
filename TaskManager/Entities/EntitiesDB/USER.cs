using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Entities.EntitiesDB
{
      /// <summary>
    /// query user
    /// </summary>
    [Table(@"USER")]
    public class USER
    {
        #region Properties
        /// <summary>
        /// sequence of user
        /// </summary>
        /// <value></value>
        [Key()]
        [Column(@"SEQUSER")]
        public virtual long SEQUSER { get; set; }
        
        /// <summary>
        /// email of user
        /// </summary>
        /// <value></value>
        [Required]
        [StringLength(120)]
        [Column(@"EMAIL")]
        public virtual string EMAIL { get; set; }

        /// <summary>
        /// password of user
        /// </summary>
        /// <value></value>
        [Required]
        [StringLength(15)]
        [Column(@"PASSWORD")]
        public virtual string PASSWORD { get; set; }

        /// <summary>
        /// name of user
        /// </summary>
        /// <value></value>
        [Required]
        [StringLength(120)]
        [Column(@"NAME")]
        public virtual string NAME { get; set; }        

        /// <summary>
        /// date of user was registered
        /// </summary>
        /// <value></value>
        [Column(@"DATEREGISTER")]
        public virtual DateTime DATEREGISTER { get; set; }

        /// <summary>
        /// last acess of user in application
        /// </summary>
        /// <value></value>
        [Column(@"LASTACESS")]
        public virtual DateTime LASTACESS { get; set; }

        /// <summary>
        /// list of task 
        /// </summary>
        /// <value></value>
        [InverseProperty("USER")]
        public virtual List<TASK> TASK { get; set; }

        #endregion    
    }
}