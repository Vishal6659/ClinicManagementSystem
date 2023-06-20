namespace ClinicManagementSystem.Helper
{
    public class QueryHelper
    {
        public const string insertRegistrationData = "Insert into tbl_newdocregistration (firstname, lastname, gender, dateofbirth, email, phonenumber, address, aadharcard, specialization, medicalliscence, qualification, experiance, affiliation, languagespoken, username, _password) values "
            + "(@Firstname, @Lastname, @Gender, @DateOfBirth, @EmailAddress, @PhoneNumber, @Address, @AadharCardNumber, @Specialization, @MedicalLiscense, @Qualificaition, @Experiance, @Affiliation, @LanguageSpoken, @Username, @Password);";

        public const string verifyLoginCredentials = "Select * from tbl_newdocregistration where username =@Username and _password =@Password;";
        public const string insertNewPatientData = "Insert into tbl_newpatient (doc_id, firstname, lastname, patientage, phone, birthday, gender, bloodgroup, address, patientweight, patientheight) values " + "(CAST(@DocId AS Int), @FirstName, @LastName, CAST(@Age AS Int), CAST(@Phone AS Int), @Birthday, @Gender, @BloodGroup, @Address, CAST(@PatientWeight AS Int), CAST(@PatientHeight AS Int));";
       // public const string getAllPatientListData = "Select id, firstname, phone, bloodgroup, birthday from tbl_newpatient where doc_id = @DocId;";
        public const string getAllPatientListData = "Select * from tbl_newpatient where doc_id = @DocId;";
    
    }
}
