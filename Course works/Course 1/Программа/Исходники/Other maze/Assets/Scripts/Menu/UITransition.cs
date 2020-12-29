using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#region
/*                               Text / Script is read-only                                     */
/*                                                                                              */
/*                          This text is protected by copyright,                                */
/*                         copying and distribution is prohibited                               */
/*                                                                                              */
/*                             Script was created by Bloodies                                   */
/*                                                                                              */
/*──────────────────────────────────────────────────────────────────────────────────────────────*/
/*──████████─────██───────────████████───████████───███████────██████───█████████───██████████──*/
/*─█░░░░░░░░█───█░░█─────────█░░░░░░░░█─█░░░░░░░░█─█░░░░░░███─█░░░░░░█─█░░░░░░░░░█─█░░░░░░░░░░█─*/
/*─█░░████░░█───█░░█─────────█░░████░░█─█░░████░░█─█░░███░░░█──██░░██──█░░███████──█░░████████──*/
/*─█░░█──█░░█───█░░█─────────█░░█──█░░█─█░░█──█░░█─█░░█──█░░█───█░░█───█░░█────────█░░█─────────*/
/*─█░░████░░███─█░░█─────────█░░█──█░░█─█░░█──█░░█─█░░█──█░░█───█░░█───█░░██████───█░░████████──*/
/*─█░░░░░░░░░░█─█░░█─────────█░░█──█░░█─█░░█──█░░█─█░░█──█░░█───█░░█───█░░░░░░░░█──█░░░░░░░░░░█─*/
/*─█░░██████░░█─█░░█─────────█░░█──█░░█─█░░█──█░░█─█░░█──█░░█───█░░█───█░░██████────████████░░█─*/
/*─█░░█────█░░█─█░░█─────────█░░█──█░░█─█░░█──█░░█─█░░█──█░░█───█░░█───█░░█────────────────█░░█─*/
/*─█░░██████░░█─█░░████████──█░░████░░█─█░░████░░█─█░░███░░░█──██░░██──█░░███████───████████░░█─*/
/*─█░░░░░░░░░░█─█░░░░░░░░░░█─█░░░░░░░░█─█░░░░░░░░█─█░░░░░░███─█░░░░░░█─█░░░░░░░░░█─█░░░░░░░░░░█─*/
/*──██████████───██████████───████████───████████──████████────██████───█████████───██████████──*/
/*──────────────────────────────────────────────────────────────────────────────────────────────*/
/*                                                                                              */
/*                          For partnership please contact here:                                */
/*                       -> bloodiesco@yandex.ru                                                */
/*                       -> kloko436@gmail.com                                                  */
/*                       -> https://vk.com/elikch                                               */
/*                       -> https://www.facebook.com/bloodiesprod                               */
/*                                                                                              */
/*                      © 20?? Elizar Chepokov All Rights Reserved                              */
#endregion

public class UITransition : MonoBehaviour 
{
	[System.Serializable]
	public class FloatEvent : UnityEvent<float>{}

	public enum Transition
	{
		FadeIn,
		FadeOut,
		SwapIn,
		SwapOut,
		Custom
	}

	public Transition transition = Transition.FadeIn;
	public AnimationCurve customTransition = AnimationCurve.EaseInOut(0, 0, 1, 1);
	public float minValue = 0f;
	public float maxValue = 1f;
	public float duration = 1f;
	public bool playOnStart = false;

	[SerializeField]
	private UnityEvent onStartedTransition = new UnityEvent();
	[SerializeField]
	private FloatEvent onChangeTransition = new FloatEvent();
	[SerializeField]
	private UnityEvent onFinishedTransition = new UnityEvent();

	private bool m_playing = false;
	private float m_time = 0f;
	private Transition m_transition;

	/// <summary>
	/// Gets the currenty value
	/// </summary>
	/// <value>The value.</value>
	public float value
	{
		get
		{
			if(this.m_transition == Transition.FadeIn)
			{
				return Mathf.Lerp(minValue, maxValue, this.progress);
			}
			else if(this.m_transition == Transition.FadeOut)
			{
				return Mathf.Lerp(maxValue, minValue, this.progress);
			}
			else if(this.m_transition == Transition.SwapIn)
			{
				return Mathf.Lerp(minValue, maxValue, Mathf.Sin(Mathf.PI / 2f * this.progress));
			}
			else if(this.m_transition == Transition.SwapOut)
			{
				return Mathf.Lerp(minValue, maxValue, Mathf.Cos(Mathf.PI / 2f * this.progress));
			}
			else
			{
				return Mathf.Lerp(minValue, maxValue, this.customTransition.Evaluate(this.progress));
			}
		}
	}

	/// <summary>
	/// Gets the progress of this Transition
	/// </summary>
	/// <value>The progress.</value>
	public float progress
	{
		get
		{
			return this.m_time / this.duration;
		}
	}

	/// <summary>
	/// Gets a value indicating whether this <see cref="UITransition"/> is playing.
	/// </summary>
	/// <value><c>true</c> if is playing; otherwise, <c>false</c>.</value>
	public bool isPlaying
	{
		get
		{
			return this.m_playing;
		}
	}

	void Start()
	{
		if(this.playOnStart)
		{
			this.Play();
		}
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		if(this.m_playing)
		{
			this.m_time += Time.deltaTime;
			if(this.m_time > this.duration)
			{
				this.m_playing = false;
				this.onFinishedTransition.Invoke();
				return;
			}
			this.onChangeTransition.Invoke(this.value);

		}
	}

	/// <summary>
	/// Play the defined transition in Inspector
	/// </summary>
	public void Play()
	{
		this.Play(this.transition);
	}

	/// <summary>
	/// Play the specified transition
	/// </summary>
	/// <param name="transition">Transition.</param>
	private void Play(Transition transition)
	{
		if(this.m_playing)
			return;

		this.m_transition = transition;
		this.m_playing = true;
		this.m_time = 0;
		this.onStartedTransition.Invoke();
	}

	/// <summary>
	/// Stop this transition.
	/// </summary>
	public void Stop()
	{
		this.m_playing = false;
	}

	/// <summary>
	/// Play transition as Fade Out.
	/// </summary>
	public void FadeIn()
	{
		this.Play(Transition.FadeIn);
	}

	/// <summary>
	/// Play transition as Fade Out.
	/// </summary>
	public void FadeOut()
	{
		this.Play(Transition.FadeOut);
	}

	/// <summary>
	/// Play transition as Swap In.
	/// </summary>
	public void SwapIn()
	{
		this.Play(Transition.SwapIn);
	}

	/// <summary>
	/// Play transition as Swap Out.
	/// </summary>
	public void SwapOut()
	{
		this.Play(Transition.SwapOut);
	}

	/// <summary>
	/// Play transition as Custom.
	/// </summary>
	public void PlayCustom()
	{
		this.Play(Transition.Custom);
	}
}
#region
/*                               Text / Script is read-only                                     */
/*                                                                                              */
/*                          This text is protected by copyright,                                */
/*                         copying and distribution is prohibited                               */
/*                                                                                              */
/*                             Script was created by Bloodies                                   */
/*                                                                                              */
/*──────────────────────────────────────────────────────────────────────────────────────────────*/
/*──████████─────██───────────████████───████████───███████────██████───█████████───██████████──*/
/*─█░░░░░░░░█───█░░█─────────█░░░░░░░░█─█░░░░░░░░█─█░░░░░░███─█░░░░░░█─█░░░░░░░░░█─█░░░░░░░░░░█─*/
/*─█░░████░░█───█░░█─────────█░░████░░█─█░░████░░█─█░░███░░░█──██░░██──█░░███████──█░░████████──*/
/*─█░░█──█░░█───█░░█─────────█░░█──█░░█─█░░█──█░░█─█░░█──█░░█───█░░█───█░░█────────█░░█─────────*/
/*─█░░████░░███─█░░█─────────█░░█──█░░█─█░░█──█░░█─█░░█──█░░█───█░░█───█░░██████───█░░████████──*/
/*─█░░░░░░░░░░█─█░░█─────────█░░█──█░░█─█░░█──█░░█─█░░█──█░░█───█░░█───█░░░░░░░░█──█░░░░░░░░░░█─*/
/*─█░░██████░░█─█░░█─────────█░░█──█░░█─█░░█──█░░█─█░░█──█░░█───█░░█───█░░██████────████████░░█─*/
/*─█░░█────█░░█─█░░█─────────█░░█──█░░█─█░░█──█░░█─█░░█──█░░█───█░░█───█░░█────────────────█░░█─*/
/*─█░░██████░░█─█░░████████──█░░████░░█─█░░████░░█─█░░███░░░█──██░░██──█░░███████───████████░░█─*/
/*─█░░░░░░░░░░█─█░░░░░░░░░░█─█░░░░░░░░█─█░░░░░░░░█─█░░░░░░███─█░░░░░░█─█░░░░░░░░░█─█░░░░░░░░░░█─*/
/*──██████████───██████████───████████───████████──████████────██████───█████████───██████████──*/
/*──────────────────────────────────────────────────────────────────────────────────────────────*/
/*                                                                                              */
/*                          For partnership please contact here:                                */
/*                       -> bloodiesco@yandex.ru                                                */
/*                       -> kloko436@gmail.com                                                  */
/*                       -> https://vk.com/elikch                                               */
/*                       -> https://www.facebook.com/bloodiesprod                               */
/*                                                                                              */
/*                      © 20?? Elizar Chepokov All Rights Reserved                              */
#endregion