using System;
using System.Collections.Generic;

namespace Mod.DungPham.KoiOctiiu957
{
    public class SkillSelectDlg : Dialog, IActionListener
    {
        public int x, y, w, h;
        public int cmx, cmtoX, cmy, cmtoY;
        public int cmyLim;
        public int selected = -1;
        public int ITEM_HEIGHT = 24;
        public List<Skill> listSkills = new List<Skill>();
        public string title = "Select Skill";
        public bool isPointerDowning;
        public int pointerDownTime;
        public int pointerDownFirstY;
        public int pointerDownLastY;
        public int pyLast;

        public SkillSelectDlg()
        {
            this.w = 170;
            this.h = 180;
            if (this.h > GameCanvas.h - 20)
            {
                this.h = GameCanvas.h - 20;
            }
            this.x = (GameCanvas.w - this.w) / 2;
            this.y = (GameCanvas.h - this.h) / 2;
            
            for (int i = 0; i < GameScr.keySkill.Length; i++)
            {
                if (GameScr.keySkill[i] != null && GameScr.keySkill[i].template != null)
                {
                    listSkills.Add(GameScr.keySkill[i]);
                }
            }
            
            this.cmyLim = listSkills.Count * ITEM_HEIGHT - (this.h - 30);
            if (this.cmyLim < 0) this.cmyLim = 0;
        }

        public override void show()
        {
            GameCanvas.currentDialog = this;
        }

        public void perform(int idAction, object p)
        {
            if (idAction == 1)
            {
                GameCanvas.currentDialog = null;
            }
        }

        public override void paint(mGraphics g)
        {
            // Modern Dark Glassmorphism Background
            g.setColor(0, 0.8f); 
            g.fillRect(this.x, this.y, this.w, this.h);
            
            // Top Accent Line
            g.setColor(52224, 1f); // Neon green
            g.fillRect(this.x, this.y, this.w, 2);
            
            // Border
            g.setColor(16777215, 0.3f);
            g.drawRect(this.x, this.y, this.w, this.h);
            
            // Header
            mFont.tahoma_7b_white.drawString(g, title, this.x + this.w / 2, this.y + 7, mFont.CENTER);
            g.setColor(16777215, 0.2f);
            g.drawLine(this.x + 10, this.y + 25, this.x + this.w - 10, this.y + 25);
            
            g.setClip(this.x, this.y + 27, this.w, this.h - 27);
            g.translate(0, -this.cmy);
            
            for (int i = 0; i < listSkills.Count; i++)
            {
                int iy = this.y + 27 + i * ITEM_HEIGHT;
                if (iy + ITEM_HEIGHT - this.cmy < this.y + 27 || iy - this.cmy > this.y + this.h) continue;
                
                bool isSelected = AutoTrain.IsSkillSelected(listSkills[i]);
                
                // Draw checkbox
                int cx = this.x + 15;
                int cy = iy + ITEM_HEIGHT / 2;
                
                if (isSelected)
                {
                    g.setColor(52224, 1f); // Green background
                    g.fillRect(cx - 6, cy - 6, 12, 12);
                    g.setColor(16777215, 1f); // White dot
                    g.fillRect(cx - 3, cy - 3, 6, 6);
                }
                else
                {
                    g.setColor(0, 0.5f); // Dark background
                    g.fillRect(cx - 6, cy - 6, 12, 12);
                    g.setColor(16777215, 0.5f); // Grey outline
                    g.drawRect(cx - 6, cy - 6, 12, 12);
                }
                
                mFont.tahoma_7b_white.drawString(g, listSkills[i].template.name, this.x + 30, iy + 6, mFont.LEFT);
            }
            
            g.translate(0, -g.getTranslateY());
            g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
            g.setColor(16777215, 1f); // Reset color
            
            base.paint(g); // Paint commands
        }
        
        public override void update()
        {
            if (GameCanvas.isPointerJustRelease && !GameCanvas.isPointer(this.x, this.y, this.w, this.h))
            {
                GameCanvas.currentDialog = null;
                GameCanvas.clearAllPointerEvent();
                return;
            }
            
            if (this.cmy != this.cmtoY)
            {
                int num = this.cmtoY - this.cmy << 2;
                num += ((num <= 0) ? -1 : 1);
                this.cmy += num / 5;
                if (Res.abs(num / 5) <= 1)
                {
                    this.cmy = this.cmtoY;
                }
            }
            
            if (GameCanvas.isPointerDown)
            {
                if (!this.isPointerDowning && GameCanvas.isPointer(this.x, this.y + 27, this.w, this.h - 27))
                {
                    this.pointerDownFirstY = GameCanvas.py;
                    this.pointerDownLastY = GameCanvas.py;
                    this.isPointerDowning = true;
                    this.pointerDownTime = 0;
                }
                if (this.isPointerDowning)
                {
                    this.pointerDownTime++;
                    this.cmtoY = this.cmy + (this.pointerDownLastY - GameCanvas.py);
                    if (this.cmtoY < 0) this.cmtoY = 0;
                    if (this.cmtoY > this.cmyLim) this.cmtoY = this.cmyLim;
                    this.pointerDownLastY = GameCanvas.py;
                }
            }
            if (GameCanvas.isPointerJustRelease && this.isPointerDowning)
            {
                this.isPointerDowning = false;
                if (Res.abs(this.pointerDownFirstY - GameCanvas.py) <= 10 && this.pointerDownTime < 10)
                {
                    int index = (this.cmy + GameCanvas.py - (this.y + 27)) / this.ITEM_HEIGHT;
                    if (index >= 0 && index < listSkills.Count)
                    {
                        Skill skill = listSkills[index];
                        int templateId = (int)skill.template.id;
                        
                        if (AutoTrain.listSelectedSkillTemplateIds.Contains(-999))
                        {
                            AutoTrain.listSelectedSkillTemplateIds.Remove(-999);
                        }
                        
                        if (AutoTrain.listSelectedSkillTemplateIds.Contains(templateId))
                        {
                            AutoTrain.listSelectedSkillTemplateIds.Remove(templateId);
                        }
                        else
                        {
                            AutoTrain.listSelectedSkillTemplateIds.Add(templateId);
                        }
                        
                        AutoTrain.SaveSkillData();
                    }
                }
                GameCanvas.clearAllPointerEvent();
            }
            
            base.update();
        }
    }
}
