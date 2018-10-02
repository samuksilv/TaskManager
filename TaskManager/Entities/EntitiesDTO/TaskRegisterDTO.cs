using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TaskManager.Entities.EntitiesDTO
{
     /// <summary>
    /// entity to manipulate users
    /// </summary>
    public class userRegisterDTO
    {
        /// <summary>
        /// email of user
        /// </summary>
        /// <value></value>
        [Required]
        [StringLength(120)]
        [JsonProperty("email")]
        public virtual string EMAIL { get; set; }

        /// <summary>
        /// password of user
        /// </summary>
        /// <value></value>
        [Required]
        [StringLength(15)]   
        [JsonProperty("password")]
        public virtual string PASSWORD { get; set; }

        /// <summary>
        /// name of user
        /// </summary>
        /// <value></value>
        [Required]
        [StringLength(120)]        
        [JsonProperty("name")]
        public virtual string NAME { get; set; }

    }
}