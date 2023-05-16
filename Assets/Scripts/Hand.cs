using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.XR.Interaction.Toolkit;

public enum HandType
{
    Left,
    Right
};

public class Hand : MonoBehaviour
{
    public HandType handType = HandType.Left;
    public bool isHidden { get; private set; } = false;

    public InputAction trackedAction = null;

    bool m_isCurrentlyTracked = false;
    
    List<MeshRenderer> m_currentRenderers = new List<MeshRenderer>();

    //

    public XRBaseInteractor interactor = null;

    private void Awake()
    {
        if (interactor == null)
        {
            interactor = GetComponentInParent<XRBaseInteractor>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        trackedAction.Enable();
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        float isTracked = trackedAction.ReadValue<float>();
        if (isTracked == 1.0f && !m_isCurrentlyTracked)
        {
            m_isCurrentlyTracked = true;
            Show();
        }
        else if (isTracked == 0 && m_isCurrentlyTracked)
        {
            m_isCurrentlyTracked = false;
            Hide();
        }
    }

    public void Show()
    {
        foreach (MeshRenderer renderer in m_currentRenderers)
        {
            renderer.enabled = true;
            m_currentRenderers.Add(renderer);
        }
        isHidden = false;
    }

    public void Hide()
    {
        m_currentRenderers.Clear();
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.enabled = false;
            m_currentRenderers.Add(renderer);
        }
        isHidden = true;
    }
}