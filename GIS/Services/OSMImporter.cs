using System.IO;
using System.Linq;
using OsmSharp;
using OsmSharp.Streams;
using NetTopologySuite.Geometries;
using GIS.Data;

namespace GIS.Services
{
    public class OSMImporter
    {
        private readonly ApplicationDbContext _context;

        public OSMImporter(ApplicationDbContext context)
        {
            _context = context;
        }

        public void ImportXml(string filePath)
        {
            using (var fileStream = File.OpenRead(filePath))
            {
                var source = new XmlOsmStreamSource(fileStream);

                var nodes = source
                    .Where(x => x.Type == OsmGeoType.Node)
                    .Cast<Node>()
                    .Where(node => node.Tags != null && node.Tags.Contains("power", "tower"));

                foreach (var node in nodes)
                {
                    var electricPole = new ElectricPole
                    {
                        Name = $"Electric Pole {node.Id}",
                        Description = "Imported from OSM",
                        Location = new Point((double)node.Longitude, (double)node.Latitude) { SRID = 4326 }
                    };

                    _context.ElectricPoles.Add(electricPole);
                }

                _context.SaveChanges();
            }
        }
    }
}
