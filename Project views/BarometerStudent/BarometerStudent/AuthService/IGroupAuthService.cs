using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IGroupAuthService 
{
	int SubjectStudentId { get;set; }

	bool CanAddStudent();

	bool CanRemoveStudent();

	bool CanChangeTutor();

}

