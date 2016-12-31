using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Utilities;

namespace Magnets {
    static class Program {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread] static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Settings.SetDefaults(new Dictionary<string, object> {
                                     {"InitialLocation", "(500, 250)"},
                                     {"InitialVelocity", "(-0.1, 0)"},
                                     {"ForceMultiplier", "10"}, {
                                                                    "Magnets", new[] {
                                                                                   "[(0,0), 100, 50, (0,0,255)]",
                                                                                   "[(0,500), 100, 50, (0,0,255)]",
                                                                                   "[(0,1000), 100, 50, (0,0,255)]",
                                                                                   "[(500,1000), 100, 50, (0,0,255)]",
                                                                                   "[(1000,1000), 100, 50, (0,0,255)]",
                                                                                   "[(1000,500), 100, 50, (0,0,255)]",
                                                                                   "[(1000,0), 100, 50, (0,0,255)]",
                                                                                   "[(500,0), 100, 50, (0,0,255)]",
                                                                               }
                                                                },
                                     {"TargetColor", new[] {255, 0, 0}},
                                     {"BaseRotation", 0.0D}
                                 });
            var form = new MagnetsForm(new Size(1000, 1000));
            Application.Run(new ControlForm(form));
        }
    }
}