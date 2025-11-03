using TMPro;
using UnityEngine;

public class MedicinePreview : MonoBehaviour
{
    public TMP_Text medicineName;
    public TMP_Text medicineDays;
    public Medicine medicine;
    public CreateRecipe createRecipe;

    void Start()
    {
        createRecipe = FindFirstObjectByType<CreateRecipe>();
    }

    public void RemoveMedicine()
    {
        createRecipe.newMedicines.Remove(medicine);
        Destroy(gameObject);
    }
}
