using System;
using UnityEngine;

namespace Mod.DungPham.KoiOctiiu957
{
    public class ZoomMod
    {
        public static float zoomRatio = 1.0f;
        public static float tempZoomRatio = 1.0f;
        public static int sliderW = 100;
        public static int sliderH = 10;
        public static bool isDragging = false;
        
        public static void PaintSlider(mGraphics g, int panelX)
        {
            int x = 60;
            int y = 41;
            g.setColor(4465162);
            g.fillRect(x, y, sliderW, sliderH);
            
            float p = (tempZoomRatio - 1.0f) / 1.5f; 
            if (p < 0) p = 0;
            if (p > 1) p = 1;
            int thumbX = x + (int)(p * sliderW);
            
            g.setColor(16777215);
            g.fillRect(thumbX - 5, y - 2, 10, sliderH + 4);
            
            mFont.tahoma_7_yellow.drawString(g, "Góc nhìn: " + (int)(tempZoomRatio * 100) + "%", x + sliderW + 10, y - 2, mFont.LEFT, mFont.tahoma_7b_dark);
        }
        
        public static void UpdateSlider(int panelX)
        {
            int globalX = panelX + 60;
            int globalY = 41;
            
            if (GameCanvas.isPointerDown)
            {
                if (GameCanvas.isPointer(globalX - 10, globalY - 10, sliderW + 20, sliderH + 20))
                {
                    isDragging = true;
                }
            }
            if (isDragging)
            {
                float p = (float)(GameCanvas.px - globalX) / sliderW;
                if (p < 0f) p = 0f;
                if (p > 1f) p = 1f;
                
                tempZoomRatio = 1.0f + p * 1.5f; // From 1.0 to 2.5
            }
            if (GameCanvas.isPointerJustRelease && isDragging)
            {
                isDragging = false;
                if (tempZoomRatio != zoomRatio)
                {
                    zoomRatio = tempZoomRatio;
                    ApplyZoom();
                }
            }
        }
        
        public static void ApplyZoom()
        {
            ScaleGUI.scaleScreen = true;
            ScaleGUI.WIDTH = (float)Screen.width * zoomRatio;
            ScaleGUI.HEIGHT = (float)Screen.height * zoomRatio;
            if (MotherCanvas.instance != null)
            {
                GameCanvas.w = MotherCanvas.instance.getWidthz();
                GameCanvas.h = MotherCanvas.instance.getHeightz();
                GameCanvas.hw = GameCanvas.w / 2;
                GameCanvas.hh = GameCanvas.h / 2;
                GameScr.loadCamera(true, -1, -1);
            }
        }
    }
}
