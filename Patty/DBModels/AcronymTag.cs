namespace Patty.DBModels
{
    internal class AcronymTag
    {
        public int AcronymId { get; set; }
        public Acronym Acronym { get; set; } = new Acronym();

        public int TagId { get; set; }
        public Tag Tag { get; set; } = new Tag();
    }
}
