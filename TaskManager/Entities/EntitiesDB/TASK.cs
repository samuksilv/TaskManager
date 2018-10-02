using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Entities.EntitiesDB
{
     /// <summary>
    /// query task
    /// </summary>
    [Table(@"TASK")]
    public class TASK
    {
        /// <summary>
        /// sequence of task
        /// </summary>
        /// <value></value>
        #region Properties        
        [Key()]
        [Column(@"SEQTASK")]
        public virtual long SEQTASK { get; set; }

        /// <summary>
        /// sequence of user
        /// </summary>
        /// <value></value>        
        [Column(@"SEQUSER")]
        [Required]
        public virtual long SEQUSER { get; set; }
        
        /// <summary>
        /// description of task
        /// </summary>
        /// <value></value>
        [Column(@"DESCRIPTION")]
        [StringLength(120)]
        public virtual string DESCRIPTION { get; set; }

        /// <summary>
        /// date of initiate task
        /// </summary>
        /// <value></value>
        [Required]
        [Column(@"DATEINITIAL")]
        public virtual DateTime DATEINITIAL { get; set; }

        /// <summary>
        /// date to waring user
        /// </summary>
        /// <value></value>
        [Column(@"WARNINGTIME")]      
        public virtual DateTime? WARNINGTIME { get; set; }

        /// <summary>
        /// local
        /// </summary>
        /// <value></value>
        [Column(@"LOCAL")]      
        [StringLength(150)]
        public virtual string LOCAL { get; set; }

        /// <summary>
        /// user 
        /// </summary>
        /// <value></value>
        [ForeignKey(@"SEQUSER")]
        public virtual USER USER { get; set; }

        #endregion
    }
}