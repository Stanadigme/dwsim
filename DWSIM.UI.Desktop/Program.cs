﻿using System;
using System.IO;
using DWSIM.UI.Controls;
using Eto.Forms;
using DWSIM.GlobalSettings;
using Cudafy;
using System.Reflection;

namespace DWSIM.UI.Desktop
{
    public class Program
    {

        [STAThread]
        public static void Main(string[] args)
        {
            MainApp(args);
        }

        [STAThread]
        public static Application MainApp(string[] args)
        {

            // sets the assembly resolver to find remaining DWSIM libraries on demand

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += new ResolveEventHandler(LoadFromNestedFolder);

            //initialize OpenTK

            if (Settings.RunningPlatform() != Settings.Platform.Mac)
            {
                OpenTK.Toolkit.Init();
            }

            // set global settings

            Settings.CultureInfo = "en";
            Settings.EnableGPUProcessing = false;
            Settings.OldUI = false;

            Exception loadsetex = null;

            try
            {
                Settings.LoadSettings("dwsim_newui.ini");
            }
            catch (Exception ex)
            {
                loadsetex = ex;
            }

            Eto.Platform platform = null;
            
            try
            {
                if (Settings.RunningPlatform() == Settings.Platform.Windows)
                {
                    switch (GlobalSettings.Settings.WindowsRenderer)
                    {
                        case Settings.WindowsPlatformRenderer.WinForms:
                            DWSIM.UI.Desktop.WinForms.StyleSetter.SetStyles();
                            platform = new Eto.WinForms.Platform();
                            platform.Add<FlowsheetSurfaceControl.IFlowsheetSurface>(() => new WinForms.FlowsheetSurfaceControlHandler());
                            platform.Add<Eto.OxyPlot.Plot.IHandler>(() => new Eto.OxyPlot.WinForms.PlotHandler());
                            platform.Add<Eto.Forms.Controls.Scintilla.Shared.ScintillaControl.IScintillaControl>(() => new Eto.Forms.Controls.Scintilla.WinForms.ScintillaControlHandler());
                            break;
                        case Settings.WindowsPlatformRenderer.WPF:
                            DWSIM.UI.Desktop.WPF.StyleSetter.SetTheme("aero", "normalcolor");
                            DWSIM.UI.Desktop.WPF.StyleSetter.SetStyles();
                            platform = new Eto.Wpf.Platform();
                            platform.Add<FlowsheetSurfaceControl.IFlowsheetSurface>(() => new WPF.FlowsheetSurfaceControlHandler());
                            platform.Add<Eto.OxyPlot.Plot.IHandler>(() => new Eto.OxyPlot.WPF2.PlotHandler());
                            platform.Add<Eto.Forms.Controls.Scintilla.Shared.ScintillaControl.IScintillaControl>(() => new Eto.Forms.Controls.Scintilla.WPF.ScintillaControlHandler());
                            break;
                        case Settings.WindowsPlatformRenderer.Gtk2:
                        case Settings.WindowsPlatformRenderer.Gtk3:
                            GlobalSettings.Settings.IsGTKRenderer = true;
                            DWSIM.UI.Desktop.GTK3.StyleSetter.SetStyles();
                            platform = new Eto.GtkSharp.Platform();
                            platform.Add<FlowsheetSurfaceControl.IFlowsheetSurface>(() => new GTK3.FlowsheetSurfaceControlHandler());
                            platform.Add<Eto.OxyPlot.Plot.IHandler>(() => new Eto.OxyPlot.Gtk3.PlotHandler());
                            //platform.Add<Eto.Forms.Controls.Scintilla.Shared.ScintillaControl.IScintillaControl>(() => new Eto.Forms.Controls.Scintilla.GTK3.ScintillaControlHandler());
                            break;
                    }
                    if (!GlobalSettings.Settings.AutomationMode)
                    {
                        new Application(platform).Run(new MainForm());
                    }
                    else
                    {
                        return new Application(platform);
                    }
                }
                else if (Settings.RunningPlatform() == Settings.Platform.Linux)
                {
                    var renderer = GlobalSettings.Settings.LinuxRenderer;
                    if (GlobalSettings.Settings.AutomationMode) renderer = Settings.LinuxPlatformRenderer.WinForms;
                    switch (renderer)
                    {
                        case Settings.LinuxPlatformRenderer.Gtk2:
                        case Settings.LinuxPlatformRenderer.Gtk3:
                            GlobalSettings.Settings.IsGTKRenderer = true;
                            DWSIM.UI.Desktop.GTK3.StyleSetter.SetStyles();
                            platform = new Eto.GtkSharp.Platform();
                            platform.Add<FlowsheetSurfaceControl.IFlowsheetSurface>(() => new GTK3.FlowsheetSurfaceControlHandler());
                            platform.Add<Eto.OxyPlot.Plot.IHandler>(() => new Eto.OxyPlot.Gtk3.PlotHandler());
                            platform.Add<Eto.Forms.Controls.Scintilla.Shared.ScintillaControl.IScintillaControl>(() => new Eto.Forms.Controls.Scintilla.GTK3.ScintillaControlHandler());
                            break;
                        case Settings.LinuxPlatformRenderer.WinForms:
                            DWSIM.UI.Desktop.WinForms.StyleSetter.SetStyles();
                            platform = new Eto.WinForms.Platform();
                            platform.Add<FlowsheetSurfaceControl.IFlowsheetSurface>(() => new WinForms.FlowsheetSurfaceControlHandler());
                            platform.Add<Eto.OxyPlot.Plot.IHandler>(() => new Eto.OxyPlot.WinForms.PlotHandler());
                            platform.Add<Eto.Forms.Controls.Scintilla.Shared.ScintillaControl.IScintillaControl>(() => new Eto.Forms.Controls.Scintilla.WinForms.ScintillaControlHandler());
                            break;
                    }
                    if (!GlobalSettings.Settings.AutomationMode)
                    {
                        new Application(platform).Run(new MainForm());
                    }
                    else
                    {
                        return new Application(platform);
                    }
                }
                else if (Settings.RunningPlatform() == Settings.Platform.Mac)
                {
                    if (GlobalSettings.Settings.AutomationMode)
                    {
                        DWSIM.UI.Desktop.GTK3.StyleSetter.SetStyles();
                        platform = new Eto.GtkSharp.Platform();
                        platform.Add<FlowsheetSurfaceControl.IFlowsheetSurface>(() => new GTK3.FlowsheetSurfaceControlHandler());
                        platform.Add<Eto.OxyPlot.Plot.IHandler>(() => new Eto.OxyPlot.Gtk3.PlotHandler());
                        platform.Add<Eto.Forms.Controls.Scintilla.Shared.ScintillaControl.IScintillaControl>(() => new Eto.Forms.Controls.Scintilla.GTK3.ScintillaControlHandler());
                    }
                    else
                    {
                        switch (GlobalSettings.Settings.MacOSRenderer)
                        {
                            case Settings.MacOSPlatformRenderer.MonoMac:
                                DWSIM.UI.Desktop.Mac.StyleSetter.SetStyles();
                                platform = new Eto.Mac.Platform();
                                DWSIM.UI.Desktop.Mac.StyleSetter.BeginLaunching();
                                platform.Add<FlowsheetSurfaceControl.IFlowsheetSurface>(() => new Mac.FlowsheetSurfaceControlHandler());
                                platform.Add<Eto.OxyPlot.Plot.IHandler>(() => new Mac.PlotHandler());
                                platform.Add<Eto.Forms.Controls.Scintilla.Shared.ScintillaControl.IScintillaControl>(() => new Eto.Forms.Controls.Scintilla.Mac.ScintillaControlHandler());
                                break;
                            case Settings.MacOSPlatformRenderer.Gtk2:
                            case Settings.MacOSPlatformRenderer.Gtk3:
                                GlobalSettings.Settings.IsGTKRenderer = true;
                                DWSIM.UI.Desktop.GTK3.StyleSetter.SetStyles();
                                platform = new Eto.GtkSharp.Platform();
                                platform.Add<FlowsheetSurfaceControl.IFlowsheetSurface>(() => new GTK3.FlowsheetSurfaceControlHandler());
                                platform.Add<Eto.OxyPlot.Plot.IHandler>(() => new Eto.OxyPlot.Gtk3.PlotHandler());
                                platform.Add<Eto.Forms.Controls.Scintilla.Shared.ScintillaControl.IScintillaControl>(() => new Eto.Forms.Controls.Scintilla.GTK3.ScintillaControlHandler());
                                break;
                            case Settings.MacOSPlatformRenderer.WinForms:
                                DWSIM.UI.Desktop.WinForms.StyleSetter.SetStyles();
                                platform = new Eto.WinForms.Platform();
                                platform.Add<FlowsheetSurfaceControl.IFlowsheetSurface>(() => new WinForms.FlowsheetSurfaceControlHandler());
                                platform.Add<Eto.OxyPlot.Plot.IHandler>(() => new Eto.OxyPlot.WinForms.PlotHandler());
                                platform.Add<Eto.Forms.Controls.Scintilla.Shared.ScintillaControl.IScintillaControl>(() => new Eto.Forms.Controls.Scintilla.WinForms.ScintillaControlHandler());
                                break;
                        }
                    }
                    var app = new Application(platform);
                    app.Initialized += (sender, e) =>
                    {
                        if (!GlobalSettings.Settings.AutomationMode)
                        {
                            if (GlobalSettings.Settings.RunningPlatform() == Settings.Platform.Mac)
                            {
                                if (GlobalSettings.Settings.MacOSRenderer == Settings.MacOSPlatformRenderer.MonoMac)
                                {
                                    DWSIM.UI.Desktop.Mac.StyleSetter.FinishedLaunching();
                                }
                            }
                            if (loadsetex != null)
                            {
                                MessageBox.Show("Error loading settings from file: " + loadsetex.Message + "\nPlease fix or remove the 'dwsim_newui.ini' from the 'Documents/DWSIM Application Data' folder and try again.", "Error", MessageBoxType.Error);
                            }
                        }
                    };
                    app.Terminating += (s, e) =>
                    {
                        if (MessageBox.Show("Do you want to close DWSIM?", "Close DWSIM", MessageBoxButtons.YesNo, MessageBoxType.Question, MessageBoxDefaultButton.Yes) == DialogResult.No)
                        {
                            e.Cancel = true;
                        }
                    };
                    if (!GlobalSettings.Settings.AutomationMode)
                    {
                        app.Run(new MainForm());
                    }
                    else
                    {
                        return app;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Logger.LogError("CPUI Error", ex);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("APP CRASH!!!");
                Console.WriteLine();
                Console.WriteLine(ex.ToString());
                string configfiledir = "";
                if (GlobalSettings.Settings.RunningPlatform() == Settings.Platform.Mac)
                {
                    configfiledir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Documents", "DWSIM Application Data");
                }
                else
                {
                    configfiledir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "DWSIM Application Data");
                }
                if (!Directory.Exists(configfiledir)) Directory.CreateDirectory(configfiledir);
                if (ex.InnerException != null)
                {
                    File.WriteAllText(System.IO.Path.Combine(configfiledir, "lasterror.txt"), ex.InnerException.ToString());
                }
                else
                {
                    File.WriteAllText(System.IO.Path.Combine(configfiledir, "lasterror.txt"), ex.ToString());
                }
            }
            return null;
        }

        static Assembly LoadFromNestedFolder(object sender, ResolveEventArgs args)
        {
            string assemblyPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "unitops", "libraries", new AssemblyName(args.Name).Name + ".dll");
            if (!File.Exists(assemblyPath))
            {
                string assemblyPath2 = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ppacks", "libraries", new AssemblyName(args.Name).Name + ".dll");
                if (!File.Exists(assemblyPath2))
                {
                    return null;
                }
                else
                {
                    Assembly assembly = Assembly.LoadFrom(assemblyPath2);
                    return assembly;
                }
            }
            else
            {
                Assembly assembly = Assembly.LoadFrom(assemblyPath);
                return assembly;
            }
        }


    }

}
