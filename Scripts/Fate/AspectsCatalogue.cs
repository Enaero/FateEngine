using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace FateEngine {
    public class AspectsCatalogue {
        private static AspectsCatalogue _singleton = null;
        private Dictionary<string, Aspect> _loadedAspects;

        public static AspectsCatalogue INSTANCE {
            get {
                if (_singleton != null) {
                    return _singleton;
                }

                Dictionary<string, Aspect> aspects = new();
                aspects.Add("Hard as a rock", new Aspect("Hard as a rock"));
                return new AspectsCatalogue(aspects);
            }
        }

        public Aspect GetAspect(string name) {
            if (_loadedAspects.ContainsKey(name)) {
                return _loadedAspects[name];
            }
            
            Logger.ERROR($"Aspect not found or loaded: {name}");
            return null;
        }

        public List<Aspect> GetAspects(string[] names) {
            if (names == null) {
                Logger.ERROR("names is null cannot GetAspects");
                return new();
            }
            
            return names.Select(
                name => GetAspect(name)
            )
            .Where(aspect => aspect != null)
            .ToList();
        }

        private AspectsCatalogue(
            Dictionary<string, Aspect> loadedAspects) 
        {
            _loadedAspects = loadedAspects;
        }
    }
}