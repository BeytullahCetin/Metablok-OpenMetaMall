﻿/* Copyright (C) Itseez3D, Inc. - All Rights Reserved
* You may not use this file except in compliance with an authorized license
* Unauthorized copying of this file, via any medium is strictly prohibited
* Proprietary and confidential
* UNLESS REQUIRED BY APPLICABLE LAW OR AGREED BY ITSEEZ3D, INC. IN WRITING, SOFTWARE DISTRIBUTED UNDER THE LICENSE IS DISTRIBUTED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OR
* CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED
* See the License for the specific language governing permissions and limitations under the License.
* Written by Itseez3D, Inc. <support@avatarsdk.com>, April 2017
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ItSeez3D.AvatarSdkSamples.Core;
using ItSeez3D.AvatarSdk.Core;

namespace MetaMall.AvatarSdk.Cloud
{
	public class ImageItem : MonoBehaviour, IPointerClickHandler
	{
		public Image image;

		public TextAsset photoAsset;

		public string fallbackUrl;

		public AvatarGender gender;

		public Action<byte[], string, AvatarGender, bool, int> imageSelectedHandler;

		public bool isCustomPlayer = false;

		public int imageIndex = 1;

		void Start()
		{
			DisplayImage();
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if(Uri.IsWellFormedUriString(fallbackUrl, UriKind.Absolute))
			{
				if (imageSelectedHandler != null)
					imageSelectedHandler(photoAsset.bytes, fallbackUrl, gender, isCustomPlayer, imageIndex);
			} else
			{
				if (imageSelectedHandler != null)
					imageSelectedHandler(photoAsset.bytes, "", gender, isCustomPlayer, imageIndex);
			}
		}

		public void DisplayImage()
		{
			if (photoAsset.bytes == null)
				return;

			Texture2D jpgTexture = new Texture2D(1, 1);
			jpgTexture.LoadImage(photoAsset.bytes);

			jpgTexture.wrapMode = TextureWrapMode.Clamp;

			var color = image.color;
			color.a = 1;
			image.color = color;

			image.preserveAspect = true;
			image.sprite = Sprite.Create(jpgTexture, new Rect(0, 0, jpgTexture.width, jpgTexture.height), Vector2.zero);
		}
	}
}
