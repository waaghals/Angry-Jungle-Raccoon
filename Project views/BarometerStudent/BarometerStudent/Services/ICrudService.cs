﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface ICrudService<T> 
{
	void Create(T subject);

	T Get(int id);

	IEnumerable<T> GetAll();

	void Delete(T subject);

	void Delete(int id);

	void Update(T subject);

}
