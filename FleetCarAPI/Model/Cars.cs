namespace FleetCarAPI.Model
{
    public class Cars
    {
        public Guid Id { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public string? Brand { get; set; }
        public string? Transmission { get; set; }
        public string? Color { get; set; }
        public string? Year { get; set; }
    }
}
