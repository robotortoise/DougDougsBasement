using System;
using System.Collections.Generic;
using System.Linq;  // Add this line
using UnityEngine;

namespace Naninovel.UI
{
    public class ModifiedGameSettingsResolutionDropdown : ScriptableDropdown
    {
        private bool allowApplySettings;

        // List of common 16:9 resolutions
        private readonly List<Resolution> fixedResolutions = new List<Resolution>
        { 

            //4k options
            new Resolution { width = 3840, height = 2160, refreshRate = 60 },
            new Resolution { width = 3840, height = 2160, refreshRate = 59 },
            new Resolution { width = 3840, height = 2160, refreshRate = 30 },
            new Resolution { width = 3840, height = 2160, refreshRate = 29 },

            //1440p options
            new Resolution { width = 2560, height = 1440, refreshRate = 60 },
            new Resolution { width = 2560, height = 1440, refreshRate = 59 },
            new Resolution { width = 2560, height = 1440, refreshRate = 30 },
            new Resolution { width = 2560, height = 1440, refreshRate = 29 },


            //1080p options
            new Resolution { width = 1920, height = 1080, refreshRate = 60 },
            new Resolution { width = 1920, height = 1080, refreshRate = 59 },
            new Resolution { width = 1920, height = 1080, refreshRate = 30 },
            new Resolution { width = 1920, height = 1080, refreshRate = 29 },

            //900p options
            new Resolution { width = 1600, height = 900, refreshRate = 60 },
            new Resolution { width = 1600, height = 900, refreshRate = 59 },
            new Resolution { width = 1600, height = 900, refreshRate = 30 },
            new Resolution { width = 1600, height = 900, refreshRate = 29 },

            //768p options
            new Resolution { width = 1366, height = 768, refreshRate = 60 },
            new Resolution { width = 1366, height = 768, refreshRate = 59 },
            new Resolution { width = 1366, height = 768, refreshRate = 30 },
            new Resolution { width = 1366, height = 768, refreshRate = 29 },

            //720p options
            new Resolution { width = 1280, height = 720, refreshRate = 60 },
            new Resolution { width = 1280, height = 720, refreshRate = 59 },
            new Resolution { width = 1280, height = 720, refreshRate = 30 },
            new Resolution { width = 1280, height = 720, refreshRate = 29 },
   
        };

        protected override void Start()
        {
            base.Start();

            #if !UNITY_STANDALONE && !UNITY_EDITOR
            transform.parent.gameObject.SetActive(false);
            #else
            InitializeOptions(fixedResolutions.Select(r => $"{r.width}x{r.height}@{r.refreshRate}Hz").ToList());
            #endif
        }

        protected override void OnValueChanged(int value)
        {
            if (!allowApplySettings) return; // Prevent changing resolution when UI initializes.
            var resolution = fixedResolutions[value];

            #if UNITY_2022_2_OR_NEWER
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, resolution.refreshRateRatio);
            #else
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, resolution.refreshRate);
            #endif
        }

        private void InitializeOptions(List<string> availableOptions)
        {
            UIComponent.ClearOptions();
            UIComponent.AddOptions(availableOptions);
            UIComponent.value = GetCurrentResolutionIndex();
            UIComponent.RefreshShownValue();
            allowApplySettings = true;
        }

        private int GetCurrentResolutionIndex()
        {
            if (fixedResolutions.Count == 0) return 0;

            var currentResolution = new Resolution { width = Screen.width, height = Screen.height, refreshRate = Screen.currentResolution.refreshRate };
            var closestResolution = fixedResolutions.Aggregate((x, y) => ResolutionDiff(x, currentResolution) < ResolutionDiff(y, currentResolution) ? x : y);
            return fixedResolutions.IndexOf(closestResolution);

            int ResolutionDiff(Resolution a, Resolution b) =>
                Mathf.Abs(a.width - b.width) + Mathf.Abs(a.height - b.height) + Mathf.Abs(a.refreshRate - b.refreshRate);
        }
    }
}
