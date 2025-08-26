using System;

namespace Cliparts.SchlagDenStarLive.MainApp.Content.GameList {

    public class Container : Messaging.Message {

        #region Properties

        public GamePool.Templates._Base.Business Business { get; set; }

        #endregion


        #region Funktionen

        public Container() { }

        public Container(
            string typeIdentifier,
            string name) {
            string subSender = "Constructor";
            Type gameType;
            try {
                if (GamePool.AvailableGames.TypeList.TryGetValue(typeIdentifier, out gameType)) this.Business = Activator.CreateInstance(gameType) as GamePool.Templates._Base.Business;
            }
            catch (Exception exc) {
                this.on_Error(subSender, exc.Message);
            }
            if (!(this.Business is GamePool.Templates._Base.Business))  this.Business = new GamePool.Default();

            this.Business.Name = name;
            //this.Business.Init();
        }

        #endregion


        #region Events.Outgoing
        #endregion


        #region Events.Incoming
        #endregion

    }
}
