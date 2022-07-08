using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System;
using Steamworks;

public class Console : MonoBehaviour
{
    public static Console Instance { get; private set; }

    public InputMaster controls;

    public SteamIntergration steamIntergration;

    public ConsoleCommand[] consoleCommands;

    public TMP_Text consoleField;
    public TMP_InputField consoleInputField;

    public Scrollbar consoleScrollbar;

    public void EnterCommand(string command)
    {
        if(command == string.Empty) { command = consoleInputField.text; }

        if (string.IsNullOrWhiteSpace(command)) { return; }

        string[] args = command.Split(' ');

        consoleInputField.text = string.Empty;

        ConsoleCommand _command = null;
        if (!CheckCommand(args, ref _command)) 
        {
            AnswerCommand($"<color=white>There is no command named <color=red>'{args[0]}'");
            return; 
        }

        consoleScrollbar.value = 1;

        ExecuteCommand(args, _command);
    }

    private void Awake()
    {
        Instance = this;

        controls = new InputMaster();

        controls.Console.Open.performed += ctx => ToggleConsole();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void ToggleConsole()
    {
        transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeSelf);
    }

    public void Update()
    {
        consoleField.GetComponent<LayoutElement>().preferredWidth = consoleField.transform.parent.GetComponent<RectTransform>().rect.width - 50;
    }

    /// <summary>
    /// <para> Returns true and sets the referenced command if command exists. </para>
    /// </summary>
    private bool CheckCommand(string[] args, ref ConsoleCommand command)
    {
        foreach (ConsoleCommand _command in consoleCommands)
        {
            if(string.Equals(args[0], _command.commandName, System.StringComparison.OrdinalIgnoreCase))
            {
                command = _command;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// <para> Adds an text in the console </para>
    /// </summary>
    public void AnswerCommand(string message)
    {
        consoleField.text += $"{message}\n";
    }

    /// <summary>
    /// <para> Executes the specified command. </para>
    /// </summary>
    private void ExecuteCommand(string[] args, ConsoleCommand command)
    {
        

        if (string.Equals(command.commandName, "ping", System.StringComparison.OrdinalIgnoreCase)) { CommandPing(); }
        if (string.Equals(command.commandName, "quit", System.StringComparison.OrdinalIgnoreCase)) { CommandQuit(); }
        if (string.Equals(command.commandName, "give_item", System.StringComparison.OrdinalIgnoreCase)) { CommandGiveItem(args); }
        if (string.Equals(command.commandName, "remove_all_items", System.StringComparison.OrdinalIgnoreCase)) { CommandRemoveAllItems(args); }
        if (string.Equals(command.commandName, "remove_item", System.StringComparison.OrdinalIgnoreCase)) { CommandRemoveItem(args); }
        if (string.Equals(command.commandName, "get_all_items", System.StringComparison.OrdinalIgnoreCase)) { CommandGetAllItems(); }
        if (string.Equals(command.commandName, "drop_random_item", System.StringComparison.OrdinalIgnoreCase)) { CommandDropRandomItem(); }

    }

    private void CommandDropRandomItem()
    {
        steamIntergration.TriggerRandomItem();
    }

    bool IsDigitsOnly(string str)
    {
        foreach (char c in str)
        {
            if (c < '0' || c > '9')
                return false;
        }

        return true;
    }

    public void CommandGetAllItems()
    {
        steamIntergration.GetAllItems();
    }

    private void CommandRemoveItem(string[] args)
    {
        if (!string.IsNullOrWhiteSpace(args[1]) && IsDigitsOnly(args[1]))
        {
            int itemDef = int.Parse(args[1]);
            steamIntergration.RemoveItem((SteamItemDef_t)itemDef);
            AnswerCommand($"<color=white>Executed Command: <color=green>remove_item!");
        }
    }

    private void CommandRemoveAllItems(string[] args)
    {
        steamIntergration.RemoveAllItems();
        AnswerCommand($"<color=white>Executed Command: <color=green>remove_all_items!");
    }

    private void CommandGiveItem(string[] args)
    {
        if(args.Length == 2)
        {
            if(string.IsNullOrWhiteSpace(args[1]) || !IsDigitsOnly(args[1])) { AnswerCommand("<color=red>Itemdef can only have numbers!"); return; }
            int itemDef = int.Parse(args[1]);
            for (int i = 0; i < steamIntergration.itemDefItem.Length; i++)
            {
                if (steamIntergration.itemDefItem[i] == (SteamItemDef_t)itemDef)
                {
                    steamIntergration.steamGetItemsAmmount[i] = 1;
                    steamIntergration.GetItem((SteamItemDef_t)itemDef);
                    AnswerCommand($"<color=green>Gave item to {SteamFriends.GetPersonaName()}");
                    return;
                }
                else if(steamIntergration.itemDefItem[i] != (SteamItemDef_t)itemDef && i == steamIntergration.itemDefItem.Length)
                {
                    AnswerCommand($"<color=red>Error: No itemDefId '{itemDef}'");
                    return;
                }
            }
        }
        else
        {
            AnswerCommand("<color=red>give_item 'itemdef'!");
        }

    }

    private void CommandQuit()
    {
#if !UNITY_EDITOR
        Application.Quit();
#endif
    }

    private void CommandPing()
    {
        AnswerCommand("<color=white>pong!");
    }
}
