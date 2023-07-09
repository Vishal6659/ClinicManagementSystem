namespace ClinicManagementSystem.Helper
{
    public class QueryHelper
    {
        public const string insertRegistrationData = "Insert into tbl_newdocregistration (firstname, lastname, gender, dateofbirth, email, phonenumber, address, aadharcard, specialization, medicalliscence, qualification, experiance, affiliation, languagespoken, username, _password) values "
            + "(@Firstname, @Lastname, @Gender, @DateOfBirth, @EmailAddress, @PhoneNumber, @Address, @AadharCardNumber, @Specialization, @MedicalLiscense, @Qualificaition, @Experiance, @Affiliation, @LanguageSpoken, @Username, @Password);";

        public const string verifyLoginCredentials = "Select * from tbl_newdocregistration where username =@Username and _password =@Password;";
        public const string insertNewPatientData = "Insert into tbl_newpatient (doc_id, patient_id, firstname, lastname, patientage, phone, birthday, gender, bloodgroup, address, patientweight, patientheight) values " + "(CAST(@DocId AS Int), CAST(@PatientId AS Int), @FirstName, @LastName, CAST(@Age AS Int), CAST(@Phone AS Int), @Birthday, @Gender, @BloodGroup, @Address, CAST(@PatientWeight AS Int), CAST(@PatientHeight AS Int));";
        public const string getAllPatientListData = "Select * from tbl_newpatient where doc_id = @DocId::bigint;";
        public const string insertNewDrugData = "insert into tbl_alldrug(doc_id, drugname, genericname, note) values " + "(CAST(@DocId AS Int), @DrugName, @GenericName, @Note);";
        public const string getAllDrugListData = "Select * from tbl_alldrug where doc_id = @DocId::bigint;";
        public const string insertNewTestData = "insert into tbl_alltests(doc_id, testname, description) values" + "(CAST(@DocId AS Int), @TestName, @Description);";
        public const string getAllTestListData = "Select * from tbl_alltests where doc_id = @DocId::bigint;";
        public const string getAllPatientCountForDashboard = "Select COUNT(*) from tbl_newPatient where doc_id = @DocId::bigint;";
        public const string getAllNewPatientCountForDashboard = "Select COUNT(CURRENT_TIMESTAMP) from tbl_newPatient where doc_id = @DocId::bigint;";

    }
}
