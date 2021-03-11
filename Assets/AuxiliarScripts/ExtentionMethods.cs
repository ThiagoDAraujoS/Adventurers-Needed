using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using Hell;
using Hell.Display;

public static class ExtentionMethods
{
    #region GameObject Extentions

    /// <summary>
    /// Delegate that suports any getComponent method
    /// </summary>
    /// <param name="t">type of the component</param>
    /// <returns>return a component got with the get component method</returns>
    private delegate T FetchComponentDelegate<T>();

    /// <summary>
    /// Delegate that suports any getComponentS methods
    /// </summary>
    /// <param name="t">the type of the component</param>
    /// <returns>the list of components</returns>
    private delegate T[] FetchComponentsDelegate<T>();

    /// <summary>
    /// Get an initialize from a class method and start it
    /// </summary>
    /// <param name="obj">the object being initialized</param>
    /// <param name="initializeArguments">the arguments for its initialization</param>
    public static void InvokeInitialize(this Component obj, object[] initializeArguments = null)
    {
        //get the right initialize method
        MethodInfo method = obj.GetType().GetMethod("Initialize", BindingFlags.NonPublic | BindingFlags.Instance);

        //if its not null initialize it
        if (method != null)
            method.Invoke(obj, initializeArguments);
    }

    /// <summary>
    /// Core logic for the initializeComponent method family, it gets a component with an getcomponent generic
    /// getComponent method and then try to initialize it
    /// </summary>
    /// <typeparam name="T">The component</typeparam>
    /// <param name="overloadedFunction">the get component function used to get the component</param>
    /// <param name="initializeArguments">the arguments for the initialization</param>
    /// <returns>the component</returns>
    private static T Fetch<T>(FetchComponentDelegate<T> overloadedFunction, object[] initializeArguments)  where T : Component
    {
        //get a component using a generic method
        T newComponent = overloadedFunction();

        //if that component is null throw an error
        if (newComponent == null)
            throw new ComponentNotFoundDuringFetchException(typeof(T).Name);

        //invoke the component initialization
        newComponent.InvokeInitialize(initializeArguments);

        //return component
        return newComponent;
    }

    /// <summary>
    /// get a list of components using any of the get componentS method
    /// and them initialize all of them
    /// </summary>
    /// <typeparam name="T">The type of the component</typeparam>
    /// <param name="overloadedFunction">the method that will get the components</param>
    /// <param name="initializeArguments">the argument to run the initialization process</param>
    /// <returns>the component initialized</returns>
    private static T[] FetchArray<T>(FetchComponentsDelegate<T> overloadedFunction, object[] initializeArguments) where T : Component
    {
        //get a component array using an generic method
        T[] newComponents = overloadedFunction();

        //create a list of T to return
        List<T> result = new List<T>();

        //if didnt found a component throw an error
        if (newComponents == null)
        {
            newComponents = new T[0];
            Debug.LogWarning("There was no " + typeof(T).Name + " to be found");
        //    throw new ComponentNotFoundDuringFetchException(typeof(T).Name);
        }

        //for each component found, initialize it and add it to the return list
        foreach (T newComponent in newComponents){
            newComponent.InvokeInitialize(initializeArguments);
            result.Add(newComponent);
        }

        //cast list into an array and return it
        return result.ToArray();
    }

    /// <summary>
    /// Fetch a component in this game object and run the initialize method if it has one
    /// </summary>
    /// <typeparam name="T">The component type</typeparam>
    /// <returns>Return the component or an error if the component is not found</returns>
    public static T InitializeComponent<T>(this GameObject value, params object[] initializeArguments) where T : Component
    {
        if (value == null)
        {
            Debug.LogError("InitializeComponent is called on a null object. Returning null...");
            return null;
        }
        return Fetch<T>(value.GetComponent<T>, initializeArguments);
    }
    
    /// <summary>
    /// Fetch a component in children and run the initialize method if it has one
    /// </summary>
    /// <typeparam name="T">The component type</typeparam>
    /// <returns>Return the component or an error if the component is not found</returns>
    public static T InitializeComponentInChildren<T>(this GameObject value, params object[] initializeArguments) where T : Component
    {
        return Fetch<T>(value.GetComponentInChildren<T>, initializeArguments);
    }

    /// <summary>
    /// Fetch a component in parent object and run the initialize method if it has one
    /// </summary>
    /// <typeparam name="T">The component type</typeparam>
    /// <returns>Return the component or an error if the component is not found</returns>
    public static T InitializeComponentInParent<T>(this GameObject value, params object[] initializeArguments) where T : Component
    {
        return Fetch<T>(value.GetComponentInParent<T>, initializeArguments);
    }

    /// <summary>
    /// Fetch an array of components in this object and run the initialize method if they have one
    /// </summary>
    /// <typeparam name="T">The component type</typeparam>
    /// <returns>Return a list of components or an error if no component was found</returns>
    public static T[] InitializeComponents<T>(this GameObject value, params object[] initializeArguments) where T : Component
    {
        return FetchArray<T>(value.GetComponents<T>, initializeArguments);
    }

    /// <summary>
    /// Fetch an array of components in children and run the initialize method if they have one
    /// </summary>
    /// <typeparam name="T">The component type</typeparam>
    /// <returns>Return a list of components or an error if no component was found</returns>
    public static T[] InitializeComponentsInChildren<T>(this GameObject value, params object[] initializeArguments) where T : Component
    {
        return FetchArray<T>(value.GetComponentsInChildren<T>, initializeArguments);
    }

    /// <summary>
    /// Instantiate an object and initialize an specific component in it
    /// </summary>
    /// <typeparam name="T">the component that will be initialized</typeparam>
    /// <param name="position">The position of the object</param>
    /// <param name="rotation">The rotation of the object</param>
    /// <param name="initializingParameters">The initialization parameters</param>
    /// <returns>Returns the component</returns>
    public static T Instantiate<T>(this GameObject value, Vector3 position, Quaternion rotation, params System.Object[] initializingParameters) where T : Component
    {
        return (GameObject.Instantiate(value, position, rotation) as GameObject).InitializeComponent<T>(initializingParameters);
    }

    /// <summary>
    /// Instantiate an object and initialize an specific component in it
    /// </summary>
    /// <typeparam name="T">The component to be initialized</typeparam>
    /// <param name="initializingParameters">The initialization parameters</param>
    /// <returns>The component that was initialized</returns>
    public static T Instantiate<T>(this GameObject value, params System.Object[] initializingParameters) where T : Component
    {
        return (GameObject.Instantiate(value) as GameObject).InitializeComponent<T>(initializingParameters);
    }
   
    #endregion

    #region C# Extentions

    /// <summary>
    /// Get a random Object inside an colection
    /// </summary>
    /// <typeparam name="T">List type</typeparam>
    /// <param name="excluded">Excluded list</param>
    /// <returns>The object or null if there's no object avaliable inside the list</returns>
    public static T Random<T>(this ICollection<T> value, Predicate<T> acceptancePredicate = null, params T[] excluded)
    {
        T result;

        List<T> randomList = value.ToList();

        do
            if (randomList.Count == 0)
                throw new NoAvaliableElementException();
            else {
                result = randomList[UnityEngine.Random.Range(0, randomList.Count)];
                randomList.Remove(result);
            }
        while (excluded.Any(element => element.Equals(result)) ||
              (acceptancePredicate != null && !acceptancePredicate(result)));

        return result;
    }

    /// <summary>
    /// Get an team inside an list using its type as index
    /// </summary>
    /// <typeparam name="T">The type to search for</typeparam>
    /// <param name="value">The team found, null if found none</param>
    /// <returns></returns>
    public static Team Get<T>(this List<Team> value) where T : Team
    {
        Team result = null;

        for (int i = 0; i < value.Count && result == null; i++)
            if (value[i].GetType() == typeof(T))
                result = value[i];

        return result;
    }

    /// <summary>
    /// Get an team inside an list using its type as index
    /// </summary>
    /// <typeparam name="SearchType">The type to search for</typeparam>
    /// <param name="value">The team found, null if found none</param>
    /// <returns></returns>
    public static T Get<T>(this List<T> value, Type searchType) 
        where T : class
    {
        T result = null;

        for (int i = 0; i < value.Count && result == null; i++)
            if (value[i].GetType() == searchType)
                result = value[i];

        return result;
    }

    /// <summary>
    /// Get an element in an array using an coord instead of 2 integers
    /// </summary>
    /// <param name="coord">the coordinate</param>
    /// <returns>the element</returns>
    public static T Get<T>(this T[,] array, Coordinate coord)
    {
        return array[coord.x, coord.y];
    }

    public delegate void Foreach2DDelegate<T>(ref T item, Coordinate coordinate);

    /// <summary>
    /// Foreach an 2D array and provides the respective Coordinate for each step of the foreach
    /// syntax myArray.Foreach2D((ref Tile tile, Coordinate coord) => { ***EXPRESSION*** });
    /// </summary>
    /// <typeparam name="T">Type of the array</typeparam>
    /// <param name="action">The action that will be done in each interation</param>
    public static void Foreach2D<T>(this T[,] array, Foreach2DDelegate<T> action)
    {
        for (int y = 0; y < array.GetLength(1); y++)
            for (int x = 0; x < array.GetLength(0); x++)
                action(ref array[x, y], new Coordinate(x, y));
    }

    /// <summary> Get the next value in a list, if its the last one move to the first one on the list </summary>
    /// <param name="index">index to get the next</param>
    /// <returns>the next element on the list</returns>
    public static T Next<T>(this List<T> array, int index)
    {
        if (++index >= array.Count)
            index = 0;

        return array[index];
    }

    /// <summary>
    /// Separate a colection into 2 arrays using a predicate
    /// </summary>
    /// <typeparam name="T">type of the colection</typeparam>
    /// <param name="arrayPositive">the array generated by the positive answer of the predicate</param>
    /// <param name="arrayNegative">this array generated by the negative answer of the predicate</param>
    /// <param name="divisionPredicate">the predicate that will test if the element should go to the positive array or the negative one</param>
    public static void Separate<T>(this ICollection<T> colection, out T[] arrayPositive, out T[] arrayNegative, Predicate<T> divisionPredicate)
    {
        List<T> arrayPositiveResult = new List<T>();
        List<T> arrayNegativeResult = new List<T>();

        foreach (T item in colection)
            if (divisionPredicate(item))
                arrayPositiveResult.Add(item);
            else
                arrayNegativeResult.Add(item);

        arrayNegative = arrayNegativeResult.ToArray();
        arrayPositive = arrayPositiveResult.ToArray();
    }

    public static void Separate<T, T1, T2>(this ICollection<T> colection, out T1[] arrayPositive, out T2[] arrayNegative, Predicate<T> divisionPredicate)
    where T1 : class
    where T2 : class
    {
        List<T1> arrayPositiveResult = new List<T1>();
        List<T2> arrayNegativeResult = new List<T2>();

        foreach (T item in colection)
            if (divisionPredicate(item))
                arrayPositiveResult.Add(item as T1);
            else
                arrayNegativeResult.Add(item as T2);

        arrayNegative = arrayNegativeResult.ToArray();
        arrayPositive = arrayPositiveResult.ToArray();
    }


    public static Direction Invert(this Direction d)
    {
        Direction result = Direction.nothing;
        switch (d)
        {
            case Direction.north:
                result = Direction.south;
                break;
            case Direction.west:
                result = Direction.east;
                break;
            case Direction.south:
                result = Direction.north;
                break;
            case Direction.east:
                result = Direction.west;
                break;
            case Direction.northeast:
                result = Direction.southwest;
                break;
            case Direction.southeast:
                result = Direction.northwest;
                break;
            case Direction.southwest:
                result = Direction.northeast;
                break;
            case Direction.northwest:
                result = Direction.northeast;
                break;
        }
        return result;
    }
    #endregion
}