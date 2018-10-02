using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TaskManager.Entities.EntitiesDTO
{
     /// <summary>
    /// query of users 
    /// </summary>
    public class UserResponseDTO {

        /// <summary>
        /// Sequence of user
        /// </summary>
        /// <value></value>
        [Required]
        [JsonProperty ("sequser")]
        public virtual long SEQUSER { get; set; }

        /// <summary>
        /// email of user
        /// </summary>
        /// <value></value>
        [Required]
        [StringLength (120)]
        [JsonProperty ("email")]
        public virtual string EMAIL { get; set; }

        /// <summary>
        /// name of user
        /// </summary>
        /// <value></value>
        [Required]
        [StringLength (120)]
        [JsonProperty ("name")]
        public virtual string NAME { get; set; }

        /// <summary>
        /// date of user was registered
        /// </summary>
        /// <value></value>
        [Required]
        [StringLength (120)]
        [JsonProperty ("dateRegister")]
        public virtual DateTime DATEREGISTER { get; set; }
    }
}