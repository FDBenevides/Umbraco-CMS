using System.Linq;
using System.Web;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace HorseSales.Persistence
{
    [TableName("HorseRequestLinkCommentDto")]
    [PrimaryKey("Id", autoIncrement = true)]
    [ExplicitColumns]
    public class HorseRequestLinkCommentDto
    {
        #region Constructors
        public HorseRequestLinkCommentDto() { }

        
        #endregion

        #region Properties

        [PrimaryKeyColumn(AutoIncrement = true)]
        [Column(Name = "id")]
        public int Id { get; set; }

        [Column(Name = "linkId")]
        [ForeignKey(typeof(HorseRequestLinkDto))]
        public int LinkId { get; set; }

        [Column(Name = "author")]
        public string Author { get; set; }

        [Column(Name = "text")]
        public string Text { get; set; }

        [Column(Name = "datetime")]
        public string Datetime { get; set; }

        #endregion
    }
}