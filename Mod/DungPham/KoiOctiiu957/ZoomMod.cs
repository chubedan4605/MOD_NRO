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
            
            float p = (tempZoomRatio - 1.0f) / 1.5f; 
            if (p < 0) p = 0;
            if (p > 1) p = 1;
            int thumbX = x + (int)(p * sliderW);
            
            int trackX = x;
            int trackY = y + (sliderH / 2) - 2;
            int trackW = sliderW;
            int trackH = 4;

            // 1. Draw entire track dark blue
            g.setColor(0x091D3E);
            g.fillRect(trackX + 1, trackY, trackW - 2, trackH);
            g.fillRect(trackX, trackY + 1, trackW, trackH - 2);

            // 2. Draw filled track orange
            int fillW = thumbX - trackX;
            if (fillW > 0)
            {
                g.setColor(0xEF6A13);
                g.fillRect(trackX + 1, trackY, fillW - 1, trackH);
                g.fillRect(trackX, trackY + 1, fillW, trackH - 2);
            }

            // 3. Draw thumb
            int cx = thumbX;
            int cy = y + (sliderH / 2);

            g.setColor(0xEF6A13);
            g.fillRect(cx - 3, cy - 5, 7, 11);
            g.fillRect(cx - 4, cy - 4, 9, 9);
            g.fillRect(cx - 5, cy - 3, 11, 7);

            // 4. Draw white chevron
            g.setColor(0xFFFFFF);
            g.fillRect(cx - 1, cy - 2, 2, 1);
            g.fillRect(cx, cy - 1, 2, 1);
            g.fillRect(cx + 1, cy, 2, 1);
            g.fillRect(cx, cy + 1, 2, 1);
            g.fillRect(cx - 1, cy + 2, 2, 1);
            
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
