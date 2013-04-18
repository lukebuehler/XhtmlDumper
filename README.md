XhtmlDumper
===========

Dumps the contents of .NET objects in Xhtml format. XhtmlDumper is intended to provide similar features as the LINQPads `Dump()` method.

This code is in beta, if you use it make sure you take a look at the code.

How it Works
------------
The `XhtmlDumper` class uses several renderers to convert the object to XHTML, the renderers can be registered in the constructor of the dumper and are tried in order they are added. Each renderer can return `true` to indicate that the renderer successfully converted the object to XHTML, and that no further renderers should be tried, or it can return `false` to make it fall back to the next renderer in line.

A renderer has to implement following interface:

    public interface IXhtmlRenderer
    {
        bool Render(object o, string description, int depth, XhtmlTextWriter writer);
    }

By default the following renderers are registered:
 * `ObjectXhtmlRenderer`: Uses reflection to print the puplic members in XHTML, this renderer is the most like LINQPads `Dump()` method.
 * `BasicXhtmlRender`: This is usually the final fallback that just does a `ToString()` on the provided object and outputs the string. 

For example, if you want to render an object that contains an image somehow, you'd just add a new renderer called something like `ImageXhtmlRenderer` and it would 
check if the object is your image type and then render it as an HTML image somehow.

Example
-------
Dumping following class:

    public enum State
    {
        Open,
        Close
    }

    public class TestObject
    {
        public int Counter;
        public int TotalCounter { get; set; }
        public decimal ProgressPercent = (decimal) 0.1156;

        public string Name { get; set; }

        public State State;
        public State? State2;

        public SubType Child;

        private int privateField = 111;

        [Browsable(false)]
        public int NotBrowsable = 222;
    }
    
Would render like this:

<table>
    <tr>
        <th colspan="2">TestObject</th>
    </tr><tr>
        <th class="left">TotalCounter</th><td>200</td>
    </tr><tr>
        <th class="left">Name</th><td>First</td>
    </tr><tr>
        <th class="left">Counter</th><td>1</td>
    </tr><tr>
        <th class="left">ProgressPercent</th><td>11.56%</td>
    </tr><tr>
        <th class="left">State</th><td>Close</td>
    </tr><tr>
        <th class="left">State2</th><td>null</td>
    </tr><tr>
        <th class="left">Child</th><td></td>
    </tr>
</table>

How to Compile and Use
----------------------
One of the easiest ways is to include the code in your C# project, or you can build the project in VisualStudio 2010 or higher and then include the XhtmlDumpler.dll.

License
-------
Released under LGPL 3.0, see COPYING.txt and COPYING.LESSER.txt for the full license.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.
