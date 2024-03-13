﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Client.MirControls;
using Client.MirGraphics;
using Client.MirNetwork;
using Client.MirObjects;
using Client.MirSounds;
using C = ClientPackets;

namespace Client.MirScenes.Dialogs
{
    public sealed class MountDialog : MirImageControl
    {
        public MirLabel MountName, MountLoyalty;
        public MirButton CloseButton, MountButton, HelpButton;
        private MirAnimatedControl MountImage;
        public MirItemCell[] Grid;

        public int StartIndex = 0;

        public MountDialog()
        {
            Index = 167;
            Library = Libraries.Prguse;
            Movable = true;
            Sort = true;
            Location = new Point(10, 30);
            BeforeDraw += MountDialog_BeforeDraw;

            MountName = new MirLabel
            {
                Location = new Point(30, 10),
                DrawFormat = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter,
                Parent = this,
                NotControl = true,
            };
            MountLoyalty = new MirLabel
            {
                Location = new Point(30, 30),
                DrawFormat = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter,
                Parent = this,
                NotControl = true,
            };

            MountButton = new MirButton
            {
                Library = Libraries.Prguse,
                Parent = this,
                Sound = SoundList.ButtonA,
                Location = new Point(262, 70)
            };
            MountButton.Click += (o, e) =>
            {
                if (CanRide())
                {
                    Ride();
                }
            };

            CloseButton = new MirButton
            {
                HoverIndex = 361,
                Index = 360,
                Library = Libraries.Prguse2,
                Parent = this,
                PressedIndex = 362,
                Sound = SoundList.ButtonA,
            };
            CloseButton.Click += (o, e) => Hide();

            HelpButton = new MirButton
            {
                Index = 257,
                HoverIndex = 258,
                PressedIndex = 259,
                Library = Libraries.Prguse2,
                Parent = this,
                Sound = SoundList.ButtonA,
            };
            HelpButton.Click += (o, e) => GameScene.Scene.HelpDialog.DisplayPage("Mounts");

            MountImage = new MirAnimatedControl
            {
                Animated = false,
                AnimationCount = 16,
                AnimationDelay = 100,
                Index = 0,
                Library = Libraries.Prguse,
                Loop = true,
                Parent = this,
                NotControl = true,
                UseOffSet = true
            };

            Grid = new MirItemCell[Enum.GetNames(typeof(MountSlot)).Length];

            Grid[(int)MountSlot.Reins] = new MirItemCell
            {
                ItemSlot = (int)MountSlot.Reins,
                GridType = MirGridType.Mount,
                Parent = this,
                Size = new Size(34, 30)

            };
            Grid[(int)MountSlot.Bells] = new MirItemCell
            {
                ItemSlot = (int)MountSlot.Bells,
                GridType = MirGridType.Mount,
                Parent = this,
                Size = new Size(34, 30)
            };

            Grid[(int)MountSlot.Saddle] = new MirItemCell
            {
                ItemSlot = (int)MountSlot.Saddle,
                GridType = MirGridType.Mount,
                Parent = this,
                Size = new Size(34, 30)
            };

            Grid[(int)MountSlot.Ribbon] = new MirItemCell
            {
                ItemSlot = (int)MountSlot.Ribbon,
                GridType = MirGridType.Mount,
                Parent = this,
                Size = new Size(34, 30)
            };


            Grid[(int)MountSlot.Mask] = new MirItemCell
            {
                ItemSlot = (int)MountSlot.Mask,
                GridType = MirGridType.Mount,
                Parent = this,
                Size = new Size(34, 30)
            };

        }

        void MountDialog_BeforeDraw(object sender, EventArgs e)
        {
            RefreshDialog();
        }

        public void RefreshDialog()
        {
            SwitchType();
            DrawMountAnimation();
        }

        private void SwitchType()
        {
            UserItem MountItem = GameScene.User.Equipment[(int)EquipmentSlot.坐骑];
            UserItem[] MountSlots = null;

            if (MountItem != null)
            {
                MountSlots = MountItem.Slots;
            }

            if (MountSlots == null) return;

            int x = 0, y = 0;

            switch (MountSlots.Length)
            {
                case 4:
                    Index = 160;
                    StartIndex = 1170;
                    MountName.Size = new Size(208, 15);
                    MountLoyalty.Size = new Size(208, 15);
                    MountImage.Location = new Point(110, 250);
                    MountButton.Index = 164;
                    MountButton.HoverIndex = 165;
                    MountButton.PressedIndex = 166;
                    MountButton.Location = new Point(210, 70);
                    CloseButton.Location = new Point(245, 3);
                    HelpButton.Location = new Point(221, 3);
                    Grid[(int)MountSlot.Mask].Visible = false;
                    x = 1; y = 1;
                    break;
                case 5:
                    Index = 167;
                    StartIndex = 1470;
                    MountName.Size = new Size(260, 15);
                    MountLoyalty.Size = new Size(260, 15);
                    MountImage.Location = new Point(0, 70);
                    MountButton.Index = 155;
                    MountButton.HoverIndex = 156;
                    MountButton.PressedIndex = 157;
                    MountButton.Location = new Point(262, 70);
                    CloseButton.Location = new Point(297, 3);
                    HelpButton.Location = new Point(274, 3);
                    Grid[(int)MountSlot.Mask].Visible = true;
                    x = 0; y = 0;
                    break;
            }

            Grid[(int)MountSlot.Reins].Location = new Point(36 + x, 323 + y);
            Grid[(int)MountSlot.Bells].Location = new Point(90 + x, 323 + y);
            Grid[(int)MountSlot.Saddle].Location = new Point(144 + x, 323 + y);
            Grid[(int)MountSlot.Ribbon].Location = new Point(198 + x, 323 + y);
            Grid[(int)MountSlot.Mask].Location = new Point(252 + x, 323 + y);
        }

        private void DrawMountAnimation()
        {
            if (GameScene.User.MountType < 0)
            {
                MountImage.Index = 0;
                MountImage.Animated = false;
            }
            else
            {
                switch (GameScene.User.MountType)
                        {
                            case 0:
                                if (GameScene.User.MountType == 0)
                                    MountImage.Index = 1170;
                                break;
                            case 1:
                                if (GameScene.User.MountType == 1)
                                    MountImage.Index = 1190;
                                break;
                            case 2:
                                if (GameScene.User.MountType == 2)
                                    MountImage.Index = 1210;
                                break;
                            case 3:
                                if (GameScene.User.MountType == 3)
                                    MountImage.Index = 1230;
                                break;
                            case 4:
                                if (GameScene.User.MountType == 4)
                                    MountImage.Index = 1250;
                                break;
                            case 5:
                                if (GameScene.User.MountType == 5)
                                    MountImage.Index = 1270;
                                break;
                            case 6:
                                if (GameScene.User.MountType == 6)
                                    MountImage.Index = 1290;
                                break;
                            case 7:
                                if (GameScene.User.MountType == 7)
                                    MountImage.Index = 1470;
                                break;
                            case 8:
                                if (GameScene.User.MountType == 8)
                                    MountImage.Index = 1490;
                                break;
                            case 9:
                                if (GameScene.User.MountType == 9)
                                    MountImage.Index = 1510;
                                break;
                            case 10:
                                if (GameScene.User.MountType == 10)
                                    MountImage.Index = 1530;
                                break;
                            case 11:
                                if (GameScene.User.MountType == 11)
                                    MountImage.Index = 1550;
                                break;
                            case 12:
                                if (GameScene.User.MountType == 12)
                                    MountImage.Index = 1450; //这里是客户端文件新添加的16张动作图片
                                break;
                            default:
                                break;
                        }
                //MountImage.Index = StartIndex + (GameScene.User.MountType * 20);//源代码这种写法简单暴力
                MountImage.Animated = true;

                UserItem item = MapObject.User.Equipment[(int)EquipmentSlot.坐骑];

                if (item != null)
                {
                    MountName.Text = item.FriendlyName;
                    MountLoyalty.Text = string.Format("{0} / {1} 忠诚度", item.CurrentDura, item.MaxDura);
                }
            }

        }

        public bool CanRide()
        {
            if (GameScene.User.MountType < 0 || GameScene.User.MountTime + 500 > CMain.Time) return false;
            if (GameScene.User.CurrentAction != MirAction.站立动作 && GameScene.User.CurrentAction != MirAction.坐骑站立) return false;

            return true;
        }

        public void Ride()
        {
            Network.Enqueue(new C.Chat { Message = "@ride" });
        }

        public override void Show()
        {
            if (Visible) return;
            if (GameScene.User.MountType < 0)
            {
                MirMessageBox messageBox = new MirMessageBox(GameLanguage.NoMount, MirMessageBoxButtons.OK);
                messageBox.Show();
                return;
            }

            Visible = true;
        }

        public MirItemCell GetCell(ulong id)
        {
            for (int i = 0; i < Grid.Length; i++)
            {
                if (Grid[i].Item == null || Grid[i].Item.UniqueID != id) continue;
                return Grid[i];
            }
            return null;
        }
    }
}
