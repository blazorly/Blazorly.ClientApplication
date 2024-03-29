﻿using Blazorly.ClientApplication.SDK.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorly.ClientApplication.SDK
{
	public enum LoginMethod
	{
		[OptionDisplay("Password")]
		Password,
		[OptionDisplay("Azure AD")]
		AzureAD,
		[OptionDisplay("Auth0")]
		Auth0,
		[OptionDisplay("Facebook")]
		Facebook,
		[OptionDisplay("Google")]
		Google,
		[OptionDisplay("Github")]
		Github
	}

	public enum PermissionLevel
	{
		None,
		All,
		Restricted
	}

	public enum FormControlType
	{
		AutoComplete,
		Button,
		CheckBox,
		CheckBoxList,
		ColorPicker,
		DatePicker,
		DropDown,
		DropDownDataGrid,
		Fieldset,
		FileInput,
		FormField,
		HTMLEditor,
		ListBox,
		Mask,
		Numeric,
		Password,
		RadioButtonList,
		Rating,
		SelectBar,
		Slider,
		SpeechToTextButton,
		SplitButton,
		Switch,
		TextArea,
		TextBox,
		Upload,
	}

	public enum PageActionAlignment
	{
		Left,
		Right,
		Center
	}

	public enum PageActionStyle
	{
        [Description("default")]
        Default,
        [Description("primary")]
        Primary,
        [Description("secondary")]
        Secondary,
        [Description("tertiary")]
        Tertiary,
        [Description("info")]
        Info,
        [Description("success")]
        Success,
        [Description("warning")]
        Warning,
        [Description("error")]
        Error,
        [Description("dark")]
        Dark,
        [Description("transparent")]
        Transparent,
        [Description("inherit")]
        Inherit,
        [Description("surface")]
        Surface
    }

	public enum PageActionVariant
	{
		Filled,
		Text,
		Outlined
	}

    public enum PageNotificationSeverity
    {
        [Description("normal")]
        Normal,
        [Description("info")]
        Info,
        [Description("success")]
        Success,
        [Description("warning")]
        Warning,
        [Description("error")]
        Error
    }

    public enum PageDialogPosition
    {
        Right,
        Left,
        Top,
        Bottom
    }
}
