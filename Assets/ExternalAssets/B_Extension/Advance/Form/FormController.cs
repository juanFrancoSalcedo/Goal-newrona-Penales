using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Features.Score;
using UnityEngine.Events;
using B_Extensions;

public class FormController : MonoBehaviour, IFormControllable
{
    [SerializeField] UnityEvent onPass;
    [SerializeField] TMP_Text textError = null;

    [RequireBadInterface(typeof(IFormInput))]
    [SerializeField] GameObject InputName;

    [RequireBadInterface(typeof(IFormInput))]
    [SerializeField] GameObject InputMail;

    [RequireBadInterface(typeof(IFormInput))]
    [SerializeField] GameObject RadialAge;

    [RequireBadInterface(typeof(IFormInput))]
    [SerializeField] GameObject Cellphone;

    [RequireBadInterface(typeof(IFormInput))]
    [SerializeField] GameObject RadialGender;

    [RequireBadInterface(typeof(IFormInput))]
    [SerializeField] GameObject ToggleUseProduct;

    [RequireBadInterface(typeof(IFormInput))]
    [SerializeField] GameObject Department;

    [RequireBadInterface(typeof(IFormInput))]
    [SerializeField] GameObject Cities;

    [RequireBadInterface(typeof(IFormInput))]
    [SerializeField] GameObject ToggleHabeas;

    [RequireBadInterface(typeof(IFormSubmitable))]
    [SerializeField] GameObject FormSubmit;

    public IFormSubmitable FormSubmitable { get=> FormSubmit.GetComponent<IFormSubmitable>(); set { } }

    private void OnEnable()
    {
        InputName.GetComponent<IFormInput>().OnUpdateState += CheckInputName;
        InputName.GetComponent<IFormInput>().OnError += ErrorInput;

        InputMail.GetComponent<IFormInput>().OnUpdateState += CheckInputEmail;
        InputMail.GetComponent<IFormInput>().OnError += ErrorInput;

        //RadialAge.GetComponent<IFormInput>().OnUpdateState += CheckInputAge;
        //RadialAge.GetComponent<IFormInput>().OnError += ErrorInput;

        Cellphone.GetComponent<IFormInput>().OnUpdateState += CheckInputCellphone;
        Cellphone.GetComponent<IFormInput>().OnError += ErrorInput;

        //RadialGender.GetComponent<IFormInput>().OnUpdateState += CheckInputGender;
        //RadialGender.GetComponent<IFormInput>().OnError += ErrorInput;

        //ToggleUseProduct.GetComponent<IFormInput>().OnUpdateState += CheckInputUseProduct;
        //ToggleUseProduct.GetComponent<IFormInput>().OnError += ErrorInput;

        //Department.GetComponent<IFormInput>().OnUpdateState += CheckDepartment;
        //Department.GetComponent<IFormInput>().OnError += ErrorInput;

        //Cities.GetComponent<IFormInput>().OnUpdateState += CheckCity;
        //Cities.GetComponent<IFormInput>().OnError += ErrorInput;

        ToggleHabeas.GetComponent<IFormInput>().OnUpdateState += CheckHabeas;
        ToggleHabeas.GetComponent<IFormInput>().OnError += ErrorInput;

        FormSubmitable.OnPass += Submit;

    }

    private void OnDisable()
    {
        InputName.GetComponent<IFormInput>().OnUpdateState -= CheckInputName;
        InputName.GetComponent<IFormInput>().OnError -= ErrorInput;

        InputMail.GetComponent<IFormInput>().OnUpdateState -= CheckInputEmail;
        InputMail.GetComponent<IFormInput>().OnError -= ErrorInput;

        //RadialAge.GetComponent<IFormInput>().OnUpdateState -= CheckInputAge;
        //RadialAge.GetComponent<IFormInput>().OnError -= ErrorInput;

        Cellphone.GetComponent<IFormInput>().OnUpdateState -= CheckInputCellphone;
        Cellphone.GetComponent<IFormInput>().OnError -= ErrorInput;

        //RadialGender.GetComponent<IFormInput>().OnUpdateState -= CheckInputGender;
        //RadialGender.GetComponent<IFormInput>().OnError -= ErrorInput;

        //ToggleUseProduct.GetComponent<IFormInput>().OnUpdateState -= CheckInputUseProduct;
        //ToggleUseProduct.GetComponent<IFormInput>().OnError -= ErrorInput;

        //Cities.GetComponent<IFormInput>().OnUpdateState -= CheckCity;
        //Cities.GetComponent<IFormInput>().OnError -= ErrorInput;

        ToggleHabeas.GetComponent<IFormInput>().OnUpdateState -= CheckHabeas;
        ToggleHabeas.GetComponent<IFormInput>().OnError -= ErrorInput;

        FormSubmitable.OnPass -= Submit;
    }


    private void Update() 
    {
        print($"{GameStateContext.State}");
        if (Input.GetKeyDown(KeyCode.Backspace) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && GameStateContext.State == GameEventType.Tutorial)
        {
            RankingManager.Instance.RegisterCurrentPlayer("guest lastname", "guest@co.com", "3330009999");
            FormSubmit.GetComponent<BaseButtonAttendant>().Click();
        }
    }

    private void CheckInputUseProduct(object obj)
    {
        ErrorInput(string.Empty);
        CheckAll();
    }

    private void CheckHabeas(object obj) => CheckAll();

    private void CheckInputCellphone(object obj)
    {
        ErrorInput(string.Empty);
        CheckAll();
    }

    private void CheckInputAge(object obj)
    {
        ErrorInput(string.Empty);
        CheckAll();
    }

    private void CheckInputEmail(object obj)
    {
        
        ErrorInput(string.Empty);
        CheckAll();
    }

    private void CheckInputName(object obj)
    {
        ErrorInput(string.Empty);
        CheckAll();
    }

    private void CheckInputGender(object obj)
    {
        ErrorInput(string.Empty);
        CheckAll();
    }

    private void CheckDepartment(object obj)
    {
        ErrorInput(string.Empty);
        CheckAll();
    }

    private void CheckCity(object obj)
    {
        ErrorInput(string.Empty);
        CheckAll();
    }

    private void ErrorInput(string obj) => textError.text = obj;

    bool[] passeds = new bool[4];
    private void CheckAll()
    {
        // ther order is necessary
        passeds[0] = InputName.GetComponent<IFormInput>().CheckComplete();
        passeds[1] = InputMail.GetComponent<IFormInput>().CheckComplete();
        passeds[2] = (bool)ToggleHabeas.GetComponent<IFormInput>().GetValue();
        passeds[3] = Cellphone.GetComponent<IFormInput>().CheckComplete();
        print($"CheckAll: {passeds[0]}, {passeds[1]}, {passeds[2]} ,{passeds[3]}");
        bool passAll = passeds.ToList().All(x => x== true);
        if (passAll)
        FormSubmitable.EnableSubmit(passAll);
    }


    public async void Submit()
    {
        string nombre = InputName.GetComponent<IFormInput>().GetValue()?.ToString() ?? string.Empty;
        string correo = InputMail.GetComponent<IFormInput>().GetValue()?.ToString() ?? string.Empty;
        string telefono = Cellphone.GetComponent<IFormInput>().GetValue()?.ToString() ?? string.Empty;
        RankingManager.Instance.RegisterCurrentPlayer(nombre, correo, telefono);
        await Task.Delay(200);
        onPass?.Invoke();
        print($"Form submitted with Name: {nombre}, Email: {correo}, Cellphone: {telefono}");
    }
}
