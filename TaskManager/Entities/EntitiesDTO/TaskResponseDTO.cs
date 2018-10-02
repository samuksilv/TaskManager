using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TaskManager.Entities.EntitiesDTO
{
    /// <summary>
    /// query of tasks
    /// </summary>
    public class TaskResponseDTO : TaskDTO {

        /// <summary>
        /// sequence of task
        /// </summary>
        /// <value></value>
        [JsonProperty ("seqtask"), Required]
        public virtual long SEQTASK { get; set; }

        /// <summary>
        /// name of user
        /// </summary>
        /// <value></value>
        [Required]
        [StringLength (120)]
        [JsonProperty ("NAME")]
        public virtual string NAME { get; set; }

    }
}