using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.SpatialMapping;

public class Iterate : MonoBehaviour {

    /// <Summary>
    /// Implements forward and backward iteration through a collection of of objects,
    /// as well as establishing a menu to reach any available painting instantaneously.
    /// </Summary>

    static int currentPainting;
    int totalPaintings;
    int[] frame;
    bool[] isBeingPlaced;
    public Canvas canvas;
    public GameObject[] paintings;

    // Use this for initialization
    void Start ()
    {
        totalPaintings = paintings.Length;
        frame = new int[totalPaintings];
        isBeingPlaced = new bool[totalPaintings];

        for (int i = 0; i < totalPaintings; i++)
        {
            isBeingPlaced[i] = this.gameObject.transform.GetChild(i).gameObject.GetComponent<TapToPlaceOnWall>().IsBeingPlaced;
            frame[i] = 0;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < totalPaintings; i++)
        {
            isBeingPlaced[i] = this.gameObject.transform.GetChild(i).gameObject.GetComponent<TapToPlaceOnWall>().IsBeingPlaced;
            if (isBeingPlaced[i] && paintings[i].activeInHierarchy)
            {
                currentPainting = this.gameObject.transform.GetChild(i).gameObject.GetComponent<TapToPlaceOnWall>().number;
            }
            //if (paintings[i].gameObject.transform.GetChild(i).gameObject.activeInHierarchy && !isBeingPlaced[i])
            //{
            //    canvas.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            //}
        }
        //if (isBeingPlaced[currentPainting])
        //{
        //    this.gameObject.transform.GetChild(currentPainting).gameObject.transform.GetChild(frame).gameObject.SetActive(false);
        //}
        //else
        //{
        //    this.gameObject.transform.GetChild(currentPainting).gameObject.transform.GetChild(frame).gameObject.SetActive(true);
        //}
    }

    public void SelectObject(int num)
    {
        if (!paintings[num].activeInHierarchy)
        {
            canvas.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            currentPainting = num;
            paintings[num].SetActive(true);
            //paintings[num].gameObject.transform.GetChild(frame[currentPainting]).gameObject.SetActive(true);
        }
    }

    public void SelectFrame(int num)
    {
        this.gameObject.transform.GetChild(currentPainting).gameObject.transform.GetChild(frame[currentPainting]).gameObject.SetActive(false);
        frame[currentPainting] = num;
        this.gameObject.transform.GetChild(currentPainting).gameObject.transform.GetChild(frame[currentPainting]).gameObject.SetActive(true);
    }

    public void Menu()
    {
        if (!canvas.gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            isBeingPlaced[currentPainting] = this.gameObject.transform.GetChild(currentPainting).gameObject.GetComponent<TapToPlaceOnWall>().IsBeingPlaced;
            if (isBeingPlaced[currentPainting])
            {
                paintings[currentPainting].SetActive(false);
            }
            canvas.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            canvas.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            canvas.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void FrameMenu()
    {
        if (!canvas.gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            if (!canvas.gameObject.transform.GetChild(1).gameObject.activeInHierarchy)
            {
                canvas.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                canvas.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }

    public void NextPainting ()
    {
        isBeingPlaced[currentPainting] = this.gameObject.transform.GetChild(currentPainting).gameObject.GetComponent<TapToPlaceOnWall>().IsBeingPlaced;
        if (isBeingPlaced[currentPainting])
        {
            paintings[currentPainting].SetActive(false);
        }
        int counter = 0;
        do
        {
            currentPainting = (currentPainting + 1) % totalPaintings;
            counter++;
        } while (paintings[currentPainting].activeInHierarchy && counter < totalPaintings);
        paintings[currentPainting].SetActive(true);
    }

    public void PreviousPainting ()
    {
        isBeingPlaced[currentPainting] = this.gameObject.transform.GetChild(currentPainting).gameObject.GetComponent<TapToPlaceOnWall>().IsBeingPlaced;
        if (isBeingPlaced[currentPainting])
        {
            paintings[currentPainting].SetActive(false);
        }
        int counter = 0;
        do
        {
            if (currentPainting - 1 < 0)
            {
                currentPainting = totalPaintings;
            }
            currentPainting--;
            counter++;
        } while (paintings[currentPainting].activeInHierarchy && counter < totalPaintings);
        paintings[currentPainting].SetActive(true);
    }

    public void NextFrame()
    {
        this.gameObject.transform.GetChild(currentPainting).gameObject.transform.GetChild(frame[currentPainting]).gameObject.SetActive(false);
        frame[currentPainting] = (frame[currentPainting] + 1) % 5;
        this.gameObject.transform.GetChild(currentPainting).gameObject.transform.GetChild(frame[currentPainting]).gameObject.SetActive(true);
    }

    public void PreviousFrame()
    {
        this.gameObject.transform.GetChild(currentPainting).gameObject.transform.GetChild(frame[currentPainting]).gameObject.SetActive(false);
        if (frame[currentPainting] == 0)
        {
            frame[currentPainting] = 5;
        }
        frame[currentPainting] = (frame[currentPainting] - 1);
        this.gameObject.transform.GetChild(currentPainting).gameObject.transform.GetChild(frame[currentPainting]).gameObject.SetActive(true);
    }
}

