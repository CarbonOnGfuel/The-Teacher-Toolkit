using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomStudentPicker : MonoBehaviour
{
    public TMP_Text studentText;
    public Button pickButton;
    public Button addButton;
    public Button removeButton;
    public Toggle removeAfterPickToggle;
    public TMP_InputField studentInputField;
    public GameObject studentListContent;
    public GameObject studentListItemPrefab;

    private List<string> students = new List<string>();

    void Start()
    {
        pickButton.onClick.AddListener(PickRandomStudent);
        addButton.onClick.AddListener(AddStudent);
        removeButton.onClick.AddListener(RemoveStudent);

        // Initialize the student list (optional)
        DisplayStudentList();
    }

    public void PickRandomStudent()
    {
        if (students.Count == 0)
        {
            studentText.text = "No students in list!";
            return;
        }

        int randomIndex = Random.Range(0, students.Count);
        string pickedStudent = students[randomIndex];
        studentText.text = pickedStudent;

        // Check if the toggle is enabled to decide if we should remove the student
        if (removeAfterPickToggle.isOn)
        {
            students.RemoveAt(randomIndex);
            DisplayStudentList(); // Refresh the list to reflect the removal
        }
    }

    public void AddStudent()
    {
        string newStudent = studentInputField.text;
        if (!string.IsNullOrWhiteSpace(newStudent) && !students.Contains(newStudent))
        {
            students.Add(newStudent);
            studentInputField.text = ""; // Clear the input field
            DisplayStudentList();
        }
    }

    public void RemoveStudent()
    {
        string studentToRemove = studentInputField.text;
        if (students.Remove(studentToRemove)) // Only remove if the student exists
        {
            studentInputField.text = ""; // Clear the input field
            DisplayStudentList();
        }
    }

    public void DisplayStudentList()
    {
        foreach (Transform child in studentListContent.transform)
        {
            Destroy(child.gameObject);
        }

        // Populate the list view with updated student names
        foreach (string student in students)
        {
            GameObject listItem = Instantiate(studentListItemPrefab, studentListContent.transform);
            TMP_Text listItemText = listItem.GetComponent<TMP_Text>();
            listItemText.text = student;
        }
    }
}
