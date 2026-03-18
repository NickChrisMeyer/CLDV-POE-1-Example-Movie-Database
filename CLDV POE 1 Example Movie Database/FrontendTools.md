# Frontend Tools — CLDV6211 Demo Reference

This file documents the four external libraries added to the MovieVault demo and explains how to find, add, and use similar tools in your own projects. All four are loaded via **CDN** (Content Delivery Network) — no installation, no npm, no build step required.

---

## 1. Introduction — Why External Libraries?

Building a professional-looking UI from scratch takes hundreds of hours. External libraries give you:

- **Consistent, tested components** (buttons, grids, icons) that work across browsers
- **Zero dependencies on your machine** — a CDN `<link>` or `<script>` tag is all you need
- **A common vocabulary** — Bootstrap classes like `col-md-4` or `btn-primary` are understood by every developer

### CDN vs Download

| Method | Pros | Cons |
|--------|------|------|
| **CDN link** | Zero setup, often cached by visitor's browser | Requires internet at runtime |
| **Download / npm** | Works offline, version-locked | Requires a build step or manual file management |

For teaching demos and rapid prototyping, CDN is always the right choice.

---

## 2. Google Fonts

**Website:** https://fonts.google.com
**What it does:** Gives you access to 1,500+ free, professionally designed typefaces.

### How to add a font

1. Go to https://fonts.google.com
2. Search for **Inter** (or any font you like)
3. Click the font → **Get font** → **Get embed code**
4. Copy the `<link>` tag and paste it into your `<head>` (after Bootstrap, before your own CSS)
5. Apply it in CSS:

```css
body {
    font-family: 'Inter', sans-serif;
}
```

The `sans-serif` fallback means if Google's servers are unreachable, the browser uses its default sans-serif font instead of failing silently.

### Weight values

The `wght@400;500;600;700` in the URL loads four weight variants:

| Value | Name | Typical use |
|-------|------|-------------|
| 400 | Regular | Body text |
| 500 | Medium | Subheadings |
| 600 | Semibold | Card titles |
| 700 | Bold | Page headings |

---

## 3. Font Awesome

**Website:** https://fontawesome.com/icons
**CDN:** https://cdnjs.cloudflare.com (search "font-awesome")
**What it does:** 5,000+ scalable vector icons, rendered as CSS pseudo-elements on `<i>` tags.

### Adding to your project

```html
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.7.2/css/all.min.css" />
```

### Using icons

```html
<i class="fa-solid fa-film"></i>
<i class="fa-solid fa-house"></i>
<i class="fa-brands fa-bootstrap"></i>
```

The class has two parts:
- **Style prefix:** `fa-solid` (filled), `fa-regular` (outline), `fa-brands` (logos)
- **Icon name:** `fa-film`, `fa-house`, `fa-calendar-days`, etc.

### Useful modifiers

```html
<i class="fa-solid fa-film fa-2x"></i>     <!-- 2× size -->
<i class="fa-solid fa-film fa-fw"></i>      <!-- fixed width (great for lists) -->
<i class="fa-solid fa-film me-1"></i>       <!-- Bootstrap margin-end for spacing -->
```

### Finding icons

1. Go to https://fontawesome.com/icons
2. Use the **Free** filter (top right)
3. Search by keyword (e.g. "film", "calendar", "building")
4. Click an icon → copy the `<i>` tag shown

---

## 4. AOS — Animate On Scroll

**Website:** https://michalsnik.github.io/aos/
**CDN CSS:** https://unpkg.com/aos@2.3.4/dist/aos.css
**CDN JS:** https://unpkg.com/aos@2.3.4/dist/aos.js
**What it does:** Triggers CSS entrance animations when elements scroll into the viewport.

### Setup (two parts)

**In `<head>`** (CSS):
```html
<link href="https://unpkg.com/aos@2.3.4/dist/aos.css" rel="stylesheet" />
```

**At the bottom of `<body>`** (JS, after your content):
```html
<script src="https://unpkg.com/aos@2.3.4/dist/aos.js"></script>
```

### Initialise in your view

Call `AOS.init()` after the library loads:

```html
@section Scripts {
<script>
    AOS.init({ duration: 700, once: true });
</script>
}
```

### AOS.init() options

| Option | Type | Default | Description |
|--------|------|---------|-------------|
| `duration` | number | 400 | Animation length in milliseconds |
| `once` | boolean | false | Animate only the first time the element enters view |
| `offset` | number | 120 | Distance (px) from viewport bottom to trigger animation |
| `easing` | string | `'ease'` | CSS easing function |
| `delay` | number | 0 | Global delay in ms (per-element overrides this) |

### Using data-aos attributes

Add `data-aos` to any HTML element:

```html
<h1 data-aos="fade-up">Animates upward on scroll</h1>
<div data-aos="fade-right" data-aos-delay="100">Delayed by 100ms</div>
<div data-aos="zoom-in" data-aos-duration="1000">Custom duration</div>
```

### Available animation types

| Type | Effect |
|------|--------|
| `fade-up` | Fades in while moving up |
| `fade-down` | Fades in while moving down |
| `fade-left` | Fades in from the right |
| `fade-right` | Fades in from the left |
| `zoom-in` | Fades in while scaling up |
| `zoom-out` | Fades in while scaling down |
| `flip-left` | 3D flip on vertical axis |
| `flip-up` | 3D flip on horizontal axis |

### Staggered animations

Apply increasing `data-aos-delay` values to siblings for a wave effect:

```html
<div data-aos="fade-up" data-aos-delay="0">Card 1 — appears first</div>
<div data-aos="fade-up" data-aos-delay="100">Card 2 — appears 100ms later</div>
<div data-aos="fade-up" data-aos-delay="200">Card 3 — appears 200ms later</div>
```

---

## 5. Bootstrap 5 — Deeper Dive

**Docs:** https://getbootstrap.com/docs/5.3
Bootstrap is already included in the ASP.NET Core MVC template. These are the features used in this demo.

### Grid System

Bootstrap divides every row into **12 columns**. `col-md-4` means "take 4 of 12 columns on medium+ screens":

```html
<div class="row g-4">           <!-- row with gap-4 gutter -->
    <div class="col-md-4">...</div>   <!-- 1/3 width on ≥768px -->
    <div class="col-md-4">...</div>
    <div class="col-md-4">...</div>
</div>
```

On screens smaller than 768px, `col-md-4` automatically becomes full width (12/12). This is the **responsive stacking** behaviour shown in verification step 5.

### Utility Classes Used in This Demo

| Class | Effect |
|-------|--------|
| `fw-bold`, `fw-semibold` | Font weight 700 / 600 |
| `mb-0`, `mb-3`, `mt-5` | Margin bottom/top (0–5 scale) |
| `d-flex gap-2 flex-wrap` | Flexbox row with wrapping |
| `text-center` | Centre-align text |
| `me-1`, `me-2` | Margin-end (right in LTR) |
| `btn btn-success btn-lg` | Large green button |
| `btn btn-outline-light` | Outlined white button |
| `small` | Font size 87.5% |

---

## 6. CSS Custom Properties (Variables)

Custom properties (also called CSS variables) let you define values once in `:root` and reference them everywhere with `var()`.

### How they work

```css
:root {
    --accent: #7bd88f;   /* defined once */
    --surface: #121622;
    --radius: 12px;
}

.feature-card {
    background: var(--surface);     /* referenced here */
    border-radius: var(--radius);   /* and here */
    border-color: var(--accent);    /* and here */
}
```

### Why they're useful

- **Theme changes in one place** — change `--accent` in `:root` and every card, icon, and hover effect updates simultaneously
- **Self-documenting** — `var(--shadow)` is clearer than `0 10px 30px rgba(0,0,0,.35)`
- **Runtime theming** — JavaScript can change variables on `:root` at runtime (dark/light mode toggle)

---

## 7. Design Tips

### Colour Contrast

Text must have sufficient contrast against its background for accessibility (WCAG AA requires a ratio of at least 4.5:1 for normal text). Check ratios at: https://webaim.org/resources/contrastchecker/

### Font Pairing

- Use **one font family** with multiple weights rather than two different families
- Inter 400 for body, Inter 600 for subheadings, Inter 700 for headings is a clean, professional pattern

### Spacing Scale

Bootstrap's spacing utilities use a 4px base unit (0=0, 1=4px, 2=8px, 3=16px, 4=24px, 5=48px). Sticking to this scale keeps layouts visually consistent.

### Hover Feedback

Every interactive element should give visual feedback on hover:

```css
.card:hover {
    transform: translateY(-4px);      /* physical lift */
    box-shadow: var(--shadow);        /* depth shadow */
    border-color: var(--accent);      /* colour highlight */
    transition: all .2s ease;         /* smooth, not instant */
}
```

---

## 8. Free Resources

| Resource | URL | Use for |
|----------|-----|---------|
| **Google Fonts** | https://fonts.google.com | Free typefaces via CDN |
| **Font Awesome** | https://fontawesome.com/icons | 5,000+ free icons |
| **Bootstrap Docs** | https://getbootstrap.com/docs/5.3 | Grid, utilities, components |
| **AOS** | https://michalsnik.github.io/aos/ | Scroll animations |
| **Coolors** | https://coolors.co | Colour palette generator |
| **Unsplash** | https://unsplash.com | Free high-quality photos |
| **Picsum** | https://picsum.photos | Placeholder images (e.g. `https://picsum.photos/800/400`) |
| **cdnjs** | https://cdnjs.cloudflare.com | Find CDN URLs for any library |
| **unpkg** | https://unpkg.com | CDN for npm packages |
| **CSS Tricks** | https://css-tricks.com | CSS guides and references |
