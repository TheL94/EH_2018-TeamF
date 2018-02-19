using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public interface IMenu
    {
        int CurrentIndexSelected { get; set; }

        List<ISelectable> SelectableButtons { get; set; }

        /// <summary>
        /// Select the current selection
        /// </summary>
        void Select();

        /// <summary>
        /// Sposta l'indice della selezione in alto.
        /// </summary>
        void GoUpInMenu();

        /// <summary>
        /// Sposta l'indice della selezione in basso.
        /// </summary>
        void GoDownInMenu();

        /// <summary>
        /// Sposta l'indice della selezione a destra.
        /// </summary>
        void GoRightInMenu();

        /// <summary>
        /// Sposta l'indice della selezione a sinistra.
        /// </summary>
        void GoLeftInMenu();

    }

    public interface ISelectable
    {
        IButtonController Controller { get; set; }
        int Index { get; set; }
        bool IsSelected { get; set; }
        void Init(int _index, IButtonController _controller);
        void ChangeImageIfSelected(bool _isSelected);
    }
}