namespace PITPortalTest.Services
{
    public class Page
    {
        public int Id { get; set; }
        public string PageName { get; set; }
        public string RouteValue { get; set; }
        public List<string> AuthorizedRoles { get; set; }
    }
}
