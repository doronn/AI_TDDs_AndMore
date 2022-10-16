namespace MergeServerData.RecipesDTO
{
    public class Recipe
    {
        public string ElementIDA { get; set; }
        public string ElementIDB { get; set; }
        public string ResultElementID { get; set; }
        public int ResultLevel { get; set; }
        public string Id { get; set; }
        public bool WasShown { get; set; }
    }
}