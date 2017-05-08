[
    {
        "name": "Rich text editor",
        "alias": "rte",
        "view": "rte",
        "icon": "icon-article"
    },
    {
        "name": "Image",
        "alias": "media",
        "view": "media",
        "icon": "icon-picture"
    },
    {
        "name": "Macro",
        "alias": "macro",
        "view": "macro",
        "icon": "icon-settings-alt"
    },
    {
        "name": "Embed",
        "alias": "embed",
        "view": "embed",
        "icon": "icon-movie-alt"
    },
    {
        "name": "Headline",
        "alias": "headline",
        "view": "textstring",
        "icon": "icon-coin",
        "config": {
            "style": "font-size: 36px; line-height: 45px; font-weight: bold",
            "markup": "<h1>#value#</h1>"
        }
    },
    {
        "name": "Quote",
        "alias": "quote",
        "view": "textstring",
        "icon": "icon-quote",
        "config": {
            "style": "border-left: 3px solid #ccc; padding: 10px; color: #ccc; font-family: serif; font-style: italic; font-size: 18px",
            "markup": "<blockquote>#value#</blockquote>"
        }
    },
    {
        "name": "test",
        "alias": "test",
        "view": "/App_Plugins/Skybrud.ImagePicker/Views/ImagePickerGridEditor.html",
        "icon": "icon-settings-alt",
        "render": "SkybrudImagePicker",
        "config": {
            "title": {
                "show": true,
                "placeholder": "partial view to use when rendering the content"
            }
        }
    },
    {
        "name": "Simple Div",
        "alias": "simpleDiv",
        "view": "textstring",
        "icon": "icon-traffic-alt",
        "config": {
            "markup": "<div class=\"#value#\"></div>",
            "style": ""
        }
    },
    {
        "name": "LeBlenderEditor",
        "alias": "leBlenderEditor",
        "view": "/App_Plugins/LeBlender/editors/leblendereditor/LeBlendereditor.html",
        "icon": "icon-mouse-cursor",
        "render": "/App_Plugins/LeBlender/editors/leblendereditor/views/Base.cshtml",
        "config": {
            "editors": [
                {
                    "name": "TestimonialName",
                    "alias": "testimonialName",
                    "propretyType": {},
                    "dataType": "0cc0eba1-9960-42c9-bf9b-60e150b429ae"
                },
                {
                    "name": "TestimonialText",
                    "alias": "testimonialText",
                    "propretyType": {},
                    "dataType": "c6bac0dd-4ab9-45b1-8e30-e4b619ee5da3"
                }
            ],
            "frontView": "testimonials",
            "max": 2,
            "renderInGrid": "1"
        }
    }
]