using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mine {
   
    public class MyTag {

        #region management

        public static MyTag GetTag(string tagName) {
            if(_tags.ContainsKey(tagName)) return _tags[tagName];
            MyTag tag = new MyTag(tagName);
            tag.parent = _tags["All"];
            _tags.Add(tagName,tag);
            return tag;
        }

        public static MyTag GetTagWithParent(string tagName, string parentTagName) {
            MyTag parentTag = MyTag.GetTag(parentTagName);
            MyTag tag = MyTag.GetTag(tagName);
            tag.Parent = parentTag;
            return tag;
        }

        public static void SetTagParent(string tagName, string parentTagName) {
            if(tagName == parentTagName) {
                Console.WriteLine("Tag and Parent Tag can't be equals");
                return;
            }
            if(!_tags.ContainsKey(tagName)) {
                Console.WriteLine(tagName + " doesn't exist!");
                return;
            }
            if(!_tags.ContainsKey(parentTagName)) {
                Console.WriteLine(parentTagName + " doesn't exist");
                return;
            }
            MyTag tag = _tags[tagName];
            MyTag parentTag = _tags[parentTagName];
            if(parentTag.parent == tag) {
                Console.WriteLine("Tag and Parent Tag already have a kinship");
                return;
            }
            tag.Parent = parentTag;
        }

        private static Dictionary<string,MyTag> _tags = new Dictionary<string,MyTag>() {
            { "All", new MyTag("All") }
        };

        #endregion

        private MyTag(string name) {
            this.name = name;
        }

        public bool HasTag(string tag) {
            if(this.name == tag) return true;
            if(this.parent != null) return this.parent.HasTag(tag);
            return false;
        }

        public string Name {
            get { return this.name; }
        }

        public MyTag Parent {
            get { return this.parent; }
            protected set { this.parent = value; }
        }

        private MyTag parent;
        private string name;
    }

}
