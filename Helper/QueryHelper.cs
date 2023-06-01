namespace ClinicManagementSystem.Helper
{
    public class QueryHelper
    {
        public const string insertRegistrationData = "Insert into tbl_newdocregistration (firstname, lastname, gender, dateofbirth, email, phonenumber, address, aadharcard, specialization, medicalliscence, qualification, experiance, affiliation, languagespoken, username, _password) values "
            + "(@Firstname, @Lastname, @Gender, @DateOfBirth, @EmailAddress, @PhoneNumber, @Address, @AadharCardNumber, @Specialization, @MedicalLiscense, @Qualificaition, @Experiance, @Affiliation, @LanguageSpoken, @Username, @Password);";

        public const string verifyLoginCredentials = "Select * from tbl_newdocregistration where username =@Username and _password =@Password;";
        public const string insertNewPatientData = "Insert into tbl_newPatient (firstname, lastname, age, phone, birthday, gender, bloodgroup, address, patientweight, patientheight) values " + "(@FirstName, @LastName, @Age, @Phone, @Birthday, @Gender, @BloodGroup, @Address, @PatientWeight, @PatientHeight);";
        public const string getAllPatientListData = "Select * from tbl_newPatient;";
    
    }
}
